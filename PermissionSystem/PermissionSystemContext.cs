// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： IPermissionSystemContext.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Mapping;
using PermissionSystem.Models;
namespace PermissionSystem
{
	public class PermissionSystemContext : DbContext
	{      
        
        public PermissionSystemContext(DbContextOptions options) : base(options)
        {
        
        }		
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            #region 数据表集合
            modelBuilder.Entity<Api>(ApiMapping.Mapping);            
            modelBuilder.Entity<Application>(ApplicationMapping.Mapping);            
            modelBuilder.Entity<ApplicationParameter>(ApplicationParameterMapping.Mapping);            
            modelBuilder.Entity<Client>(ClientMapping.Mapping);            
            modelBuilder.Entity<ClientApi>(ClientApiMapping.Mapping);            
            modelBuilder.Entity<ClientGrantTypes>(ClientGrantTypesMapping.Mapping);            
            modelBuilder.Entity<ClientUris>(ClientUrisMapping.Mapping);            
            modelBuilder.Entity<GrantType>(GrantTypeMapping.Mapping);            
            modelBuilder.Entity<Log>(LogMapping.Mapping);            
            modelBuilder.Entity<Menu>(MenuMapping.Mapping);            
            modelBuilder.Entity<Organization>(OrganizationMapping.Mapping);            
            modelBuilder.Entity<PayOrder>(PayOrderMapping.Mapping);            
            modelBuilder.Entity<PaySerial>(PaySerialMapping.Mapping);            
            modelBuilder.Entity<Role>(RoleMapping.Mapping);            
            modelBuilder.Entity<User>(UserMapping.Mapping);            
            modelBuilder.Entity<UserRole>(UserRoleMapping.Mapping);            
            modelBuilder.Entity<UserRoleJurisdiction>(UserRoleJurisdictionMapping.Mapping);            
    		#endregion      
            base.OnModelCreating(modelBuilder);
        }
		
        #region 数据表集合
		public DbSet<Api> Api { get; set; }
		public DbSet<Application> Application { get; set; }
		public DbSet<ApplicationParameter> ApplicationParameter { get; set; }
		public DbSet<Client> Client { get; set; }
		public DbSet<ClientApi> ClientApi { get; set; }
		public DbSet<ClientGrantTypes> ClientGrantTypes { get; set; }
		public DbSet<ClientUris> ClientUris { get; set; }
		public DbSet<GrantType> GrantType { get; set; }
		public DbSet<Log> Log { get; set; }
		public DbSet<Menu> Menu { get; set; }
		public DbSet<Organization> Organization { get; set; }
		public DbSet<PayOrder> PayOrder { get; set; }
		public DbSet<PaySerial> PaySerial { get; set; }
		public DbSet<Role> Role { get; set; }
		public DbSet<User> User { get; set; }
		public DbSet<UserRole> UserRole { get; set; }
		public DbSet<UserRoleJurisdiction> UserRoleJurisdiction { get; set; }
		#endregion
		
		
   }
}













