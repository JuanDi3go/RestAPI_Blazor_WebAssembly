using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Features.Departments.Queries.GetAll
{
    public class GetAllDepartmentsQuery:IRequest<PagedResponse<List<DepartmentDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
    }

    public class GetAllDeparmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, PagedResponse<List<DepartmentDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Department> _repositoryAsync;
        private readonly IDistributedCache _distrubitedCache;

        public GetAllDeparmentsQueryHandler(IRepositoryAsync<Department> repositoryAsync, IMapper mapper, IDistributedCache distrubitedCache)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _distrubitedCache = distrubitedCache;
        }

        public async Task<PagedResponse<List<DepartmentDTO>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {

            var cacheKey = $"DepartmentsList_{request.Name}_{request.PageNumber}_{request.PageSize}";
            string serializedListDepartment;
            var listDepartment = new List<Department>();

            var redisListDepartment = await _distrubitedCache.GetAsync(cacheKey);

            if (redisListDepartment != null)
            {
                serializedListDepartment = Encoding.UTF8.GetString(redisListDepartment);
                listDepartment = JsonConvert.DeserializeObject<List<Department>>(serializedListDepartment);
            }
            else
            {
                var query = await _repositoryAsync.GetAll();

                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(p => p.Name.Contains(request.Name)).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
                }
                else
                {
                    query = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
                }
                listDepartment = query.ToList();

                serializedListDepartment = JsonConvert.SerializeObject(listDepartment);
                redisListDepartment = Encoding.UTF8.GetBytes(serializedListDepartment);

                var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distrubitedCache.SetAsync(cacheKey, redisListDepartment, options);
            }
    
            var dtoResponse = _mapper.Map<List<DepartmentDTO>>(listDepartment);

            return new PagedResponse<List<DepartmentDTO>>(dtoResponse, request.PageNumber, request.PageSize);
            
        }
    }
}
