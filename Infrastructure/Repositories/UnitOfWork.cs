using Application.Abstractions;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<int> CompleteAsync()
        {
          return  await _context.SaveChangesAsync();
        }
    }
}
