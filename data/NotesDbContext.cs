﻿using Microsoft.EntityFrameworkCore;
using Notes.Api.Model.Entities;

namespace Notes.Api.data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Note> Notes { get; set; }
    }
}
