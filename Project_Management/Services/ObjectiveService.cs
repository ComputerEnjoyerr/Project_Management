using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models;
using System;

namespace Project_Management.Services
{
    public interface IObjectiveService
    {

    }

    public class ObjectiveService : IObjectiveService
    {
        private readonly ProjectManagementDbContext _db;
        private readonly ApplicationDbContext applicationDb;
        public ObjectiveService(ProjectManagementDbContext db,
            ApplicationDbContext applicationDb) { 
            _db = db; 
            this.applicationDb = applicationDb;
        }

    }
}
