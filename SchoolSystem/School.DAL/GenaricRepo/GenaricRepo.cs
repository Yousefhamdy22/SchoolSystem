using Microsoft.EntityFrameworkCore;
using SchoolSystem.DTOs;
using SchoolSystem.School.DAL.Data.Context;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace SchoolSystem.School.DAL.GenaricRepo
{
    public class GenaricRepo<T> : IGenaricRepo<T> where T : class
    {
        protected readonly SchoolContext context;

        public GenaricRepo(SchoolContext _context)
        {
            context = _context;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            try
            {
              var entity = await context.Set<T>().FindAsync(id);
                
                if (entity == null)
                {
                    throw new KeyNotFoundException($"No entity found with ID {id}.");
                }
                return entity;

            }
            catch
            {
                throw new Exception("error");
            }
           
        }

        public async Task<ResponseModel<object>> InsertAsync(T entity)
        {
            try
            {
                await context.Set<T>().AddAsync(entity);
            }
            catch { throw new Exception("error insert"); }
            return new ResponseModel<object> { Success = true, Message = "Inserted successfully", Status = 200 }; 
            
          
        }

        public async Task<ResponseModel<object>> UpdateAsync(T entity)
        {
            //try
            //{
            //    this.context.Entry(entity).State = EntityState.Modified; //for handle map
            //    context.Update(entity);
            //    //  this.wiredContext.tblHistory.Add();
            //    await context.SaveChangesAsync();
            //    //context.Set<T>().Update(entity);
            //   await context.SaveChangesAsync();//  
            //    return new ResponseModel<object> { Success = true, Message = "Updated successfully", Status = 200 };
            //}
            //catch(Exception ex) { throw new Exception("error"); }


            try
            {

                context.Entry(entity).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return new ResponseModel<object> { Success = true, Message = "Updated successfully", Status = 200 };
            }
            catch (DbUpdateException dbUpdateEx)
            {
                return new ResponseModel<object> { Success = false, Message = "Database update failed", Status = 500 };
            }
            
        }

        public async Task<ResponseModel<object>> DeleteAsync(object id)
        {

            try
            {
                var entity = await GetByIdAsync(id);  // catch
                if (entity == null)
                {
                    return new ResponseModel<object> { Success = false, Message = "Not found", Status = 404 };
                }
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync(); //
                return new ResponseModel<object> { Success = true, Message = "Deleted successfully", Status = 200 };
            }
            catch (Exception ex)
            {
             
                throw new Exception($"Delete Error: {ex.Message}", ex);
            }

            
        }

        public async Task SaveChangesAsync()
        {
            //  await context.SaveChangesAsync();
            try
            {
                await context.SaveChangesAsync();
                //return true; 
            }
            catch (DbUpdateException ex)
            {

                Console.WriteLine($"An error occurred while saving changes: {ex.Message}");
                // return false;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
        }
    }
}
