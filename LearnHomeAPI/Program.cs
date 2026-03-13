using LearnHomeAPI.Applications.Service;
using LearnHomeAPI.Contexts;
using LearnHomeAPI.Interfaces;
using LearnHomeAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conex�o com o banco de dados
builder.Services.AddDbContext<LearnHomeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Aluno
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<AlunoService>();

//AreaExpecializacao
builder.Services.AddScoped<IAreaEspecializacaoRepository, AreaEspecializacaoRepository>();
builder.Services.AddScoped<AreaEspecializacaoService>();

//Curso
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<CursoService>();

//Curso
builder.Services.AddScoped<ICursoAlunoRepository, CursoAlunoRepository>();
builder.Services.AddScoped < CursoAlunoService>();

//Instrutor
builder.Services.AddScoped<IInstrutorRepository, InstrutorRepository>();
builder.Services.AddScoped<InstrutorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
