using Npgsql;
using Dapper;
using Domain.Wrapper;
using Domain.Entities;
using Infrastructure.DataContext;

public class MentorService
{
   private DataContext _context;
     public MentorService (DataContext context)
    {
        _context = context;
    }

     public async Task<Response<List<Mentor>>> GetMentors()
    {
        await using var connection = _context.CreateConnection();
        var sql = "select  * from Mentor";
        var result = await connection.QueryAsync<Mentor>(sql);
        return new Response<List<Mentor>>(result.ToList());
    }

     public async Task<Response<Mentor>> AddMentor(Mentor mentor)
    {
        using var connection = _context.CreateConnection();
        try
        {
            var sql = "insert into Mentor (FirstName, LastName, Email, Phone, Address, City) values (@FirstName, @LastName, @Email, @Phone, @Address, @City) returning id;";
            var result = await connection.ExecuteScalarAsync<int>(sql, new { mentor.FirstName, mentor.LastName, mentor.Email, mentor.Phone, mentor.Address, mentor.City});
            mentor.Id = result;
            return new Response<Mentor>(mentor);
        }
        catch (Exception ex)
        {
            return new Response<Mentor>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

      public async Task<Response<Mentor>> UpdateMentor(Mentor mentor)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
           var sql = "insert into Mentor (FirstName, LastName, Email, Phone, Address, City) values (@FirstName, @LastName, @Email, @Phone, @Address, @City) returning id;";
            var result = await connection.ExecuteScalarAsync<int>(sql, new { mentor.FirstName, mentor.LastName, mentor.Email, mentor.Phone, mentor.Address, mentor.City});
            mentor.Id = result;
            return new Response<Mentor>(mentor);
        }
        }
         catch (Exception e)
        {     
           return new Response<Mentor>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }  
       
    }

    public async Task<Response<int>> DeleteMentor(int id)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"delete from Mentor where Id = {id}";
            var response  = await connection.ExecuteScalarAsync<int>(sql);
            id = response;
            return new Response<int>(response);
        }
        }
         catch (Exception e)
        {
           return new Response<int>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    } 
}
