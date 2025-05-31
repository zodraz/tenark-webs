using OpenId2Ids.Localization;
using Tenark.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace OpenId2Ids.Permissions;

public class OpenId2IdsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(OpenId2IdsPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(OpenId2IdsPermissions.MyPermission1, L("Permission:MyPermission1"));

        //var myGroup = context.AddGroup(TenarkPermissions.GroupName);

        var booksPermission = myGroup.AddPermission(TenarkPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(TenarkPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(TenarkPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(TenarkPermissions.Books.Delete, L("Permission:Books.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(TenarkPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<OpenId2IdsResource>(name);
    }
}
