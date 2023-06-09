﻿using SwipperBackup.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwipperBackup.Models;
using Microsoft.EntityFrameworkCore;

namespace SwipperBackup;
public class Startup
{
    private IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

        services.AddDbContext<DatabaseContext>(options =>
            options.UseMySql(_configuration.GetConnectionString("DefaultConnection"), serverVersion));

        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();


        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("_myAllowSpecificOrigins");

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}