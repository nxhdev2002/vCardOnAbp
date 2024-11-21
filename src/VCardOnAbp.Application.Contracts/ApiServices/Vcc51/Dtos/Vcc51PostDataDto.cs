namespace VCardOnAbp.ApiServices.Vcc51.Dtos;
public class Vcc51PostDataDto
{
    public virtual string? __LASTFOCUS { get; set; }
    public virtual string? __EVENTTARGET { get; set; }
    public virtual string? __EVENTARGUMENT { get; set; }
    public virtual string? __VIEWSTATE { get; set; }
    public virtual string? __VIEWSTATEGENERATOR { get; set; }
    public virtual string? __EVENTVALIDATION { get; set; }
}

public class Vcc51PostDataCreateCardDto : Vcc51PostDataDto
{
    public virtual string? ddlqd { get; set; }
    public virtual string? xxList { get; set; }
    public virtual string? txtkamoney { get; set; }
    public virtual string? ddljine { get; set; }
    public virtual string? txtkanum { get; set; }
    public virtual string? ddlcu { get; set; }
    public virtual string? ddlyg { get; set; }
    public virtual string? txtbz { get; set; }
    public virtual string? btnSave { get; set; }
    public virtual string? lblkainum { get; set; }
    public virtual string? iskanum { get; set; }
    public virtual string? lblkaifei { get; set; }
    public virtual string? iskafei { get; set; }
    public virtual string? isbizhong { get; set; }
    public virtual string? lblczfy { get; set; }
    public virtual string? lblczzd { get; set; }
    public virtual string? isczfy { get; set; }
    public virtual string? isczzd { get; set; }
    public virtual string? issky { get; set; }
    public virtual string? lblallfei { get; set; }
    public virtual string? isallfei { get; set; }
    public virtual string? isyue { get; set; }
}

public class Vcc51PostDataGetCardDto : Vcc51PostDataDto
{
    public virtual string start { get; set; }
    public virtual string txtName { get; set; }
    public virtual string ddlcp { get; set; }
    public virtual string ddlzt { get; set; }
    public virtual int? ddlsh { get; set; }
    public virtual string ddlyg { get; set; }
    public virtual int? AspNetPager1_input { get; set; }
    public virtual int? ddlPageSize { get; set; }
}