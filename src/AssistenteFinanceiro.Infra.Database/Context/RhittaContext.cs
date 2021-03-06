﻿using AssistenteFinanceiro.Domain.Model.Contas;
using AssistenteFinanceiro.Domain.Model.Transacoes;
using AssistenteFinanceiro.Infra.Database.Configuration;
using AssistenteFinanceiro.Infra.Database.MappingConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AssistenteFinanceiro.Infra.Database.Context
{
    public class RhittaContext : DbContext
    {
        private readonly DatabaseSettings _settings;
        private readonly ILoggerFactory _loggerFactory;

        public RhittaContext(IOptions<DatabaseSettings> settings, ILoggerFactory loggerFactory)
        {
            _settings = settings.Value;
            _loggerFactory = loggerFactory;
        }

        public DbSet<Conta> Contas { get; private set; }
        public DbSet<Transacao> Transacoes { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfiguration(new ContasConfiguration());
            modelBuilder.ApplyConfiguration(new TransacoesConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.UseNpgsql(_settings.ConnectionString);
        }
    }
}
