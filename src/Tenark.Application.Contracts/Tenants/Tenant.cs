//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JetBrains.Annotations;
//using Volo.Abp.Auditing;
//using Volo.Abp.Domain.Entities.Auditing;

//namespace Volo.Abp.TenantManagement;

//public class MyTenant : FullAuditedAggregateRoot<Guid>, IHasEntityVersion
//{
//    public virtual string Name { get; protected set; }

//    public virtual string NormalizedName { get; protected set; }

//    public virtual int EntityVersion { get; protected set; }

//    public virtual List<TenantConnectionString> ConnectionStrings { get; protected set; }

//    protected Tenant()
//    {

//    }

//    protected internal Tenant(Guid id, [NotNull] string name, [CanBeNull] string normalizedName)
//        : base(id)
//    {
//        SetName(name);
//        SetNormalizedName(normalizedName);

//        ConnectionStrings = new List<TenantConnectionString>();
//    }

//    [CanBeNull]
//    public virtual string FindDefaultConnectionString()
//    {
//        return FindConnectionString(Data.ConnectionStrings.DefaultConnectionStringName);
//    }

//    [CanBeNull]
//    public virtual string FindConnectionString(string name)
//    {
//        return ConnectionStrings.FirstOrDefault(c => c.Name == name)?.Value;
//    }

//    public virtual void SetDefaultConnectionString(string connectionString)
//    {
//        SetConnectionString(Data.ConnectionStrings.DefaultConnectionStringName, connectionString);
//    }

//    public virtual void SetConnectionString(string name, string connectionString)
//    {
//        var tenantConnectionString = ConnectionStrings.FirstOrDefault(x => x.Name == name);

//        if (tenantConnectionString != null)
//        {
//            tenantConnectionString.SetValue(connectionString);
//        }
//        else
//        {
//            ConnectionStrings.Add(new TenantConnectionString(Id, name, connectionString));
//        }
//    }

//    public virtual void RemoveDefaultConnectionString()
//    {
//        RemoveConnectionString(Data.ConnectionStrings.DefaultConnectionStringName);
//    }

//    public virtual void RemoveConnectionString(string name)
//    {
//        var tenantConnectionString = ConnectionStrings.FirstOrDefault(x => x.Name == name);

//        if (tenantConnectionString != null)
//        {
//            ConnectionStrings.Remove(tenantConnectionString);
//        }
//    }

//    protected internal virtual void SetName([NotNull] string name)
//    {
//        Name = Check.NotNullOrWhiteSpace(name, nameof(name), TenantConsts.MaxNameLength);
//    }

//    protected internal virtual void SetNormalizedName([CanBeNull] string normalizedName)
//    {
//        NormalizedName = normalizedName;
//    }
//}
