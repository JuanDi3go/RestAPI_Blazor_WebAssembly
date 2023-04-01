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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetAll
{
    public class GetAllEmployeesQuery: IRequest<PagedResponse<List<EmployeeDTO>>>
    {
        public string? Name { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    
    }

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, PagedResponse<List<EmployeeDTO>>>
{
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Employee> _repository;
        public readonly IDistributedCache _distributedCache;

        public GetAllEmployeesQueryHandler(IRepositoryAsync<Employee> repository, IMapper mapper, IDistributedCache distributedCache)
        {
            _repository = repository;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<PagedResponse<List<EmployeeDTO>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {

            string cacheKey = $"ListEmployee_{request.Name}_{request.PageNumber}_{request.PageSize}";
            List<Employee> listEmployee = new List<Employee>();
            var redisCache = await _distributedCache.GetAsync(cacheKey);
            string serializedRedisEployee;

            if (redisCache != null)
            {
                serializedRedisEployee = Encoding.UTF8.GetString(redisCache);
                listEmployee = JsonConvert.DeserializeObject<List<Employee>>(serializedRedisEployee);
            }
            else
            {
                var query = await _repository.GetAll();

                if (!string.IsNullOrEmpty(request.Name))
                    query = query.Where(p => p.Name.Contains(request.Name));

                query = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
                listEmployee = query.ToList();

                serializedRedisEployee = JsonConvert.SerializeObject(listEmployee);
                redisCache = Encoding.UTF8.GetBytes(serializedRedisEployee);

                var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync(cacheKey, redisCache, options);
            }


            var dtoResponse = _mapper.Map<List<EmployeeDTO>>(listEmployee);

            return new PagedResponse<List<EmployeeDTO>>(dtoResponse, request.PageNumber, request.PageSize);
        }
    }
}
