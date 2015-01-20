﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Piranha.Areas.Manager.Views.PageTypeMgr
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using Piranha;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Manager/Views/PageTypeMgr/Edit.cshtml")]
    public partial class Edit : System.Web.Mvc.WebViewPage<Guid?>
    {
        public Edit()
        {
        }
        public override void Execute()
        {
DefineSection("script", () => {

WriteLiteral("\r\n\t<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\t\tko.applyBindings(new manager.models.pagetype(\'");

            
            #line 5 "..\..\Areas\Manager\Views\PageTypeMgr\Edit.cshtml"
                                                 Write(Model);

            
            #line default
            #line hidden
WriteLiteral("\', {\r\n\t\t\trequired: \'Field is required\',\r\n\t\t\tunique: \'Field has to be unique\'\r\n\t\t}" +
"));\r\n\t</script>\r\n");

});

WriteLiteral("\r\n");

            
            #line 12 "..\..\Areas\Manager\Views\PageTypeMgr\Edit.cshtml"
 using (var form = Html.BeginForm("Save", "PageTypeMgr", FormMethod.Post, new { @class = "form", @role = "form" })) {

            
            #line default
            #line hidden
WriteLiteral("\t<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n\t\t<div");

WriteLiteral(" class=\"col-sm-12 buttons\"");

WriteLiteral(">\r\n\t\t\t<!-- Actions -->\r\n\t\t\t<div");

WriteLiteral(" class=\"btn-group\"");

WriteLiteral(">\r\n\t\t\t\t<button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn btn-labeled btn-success\"");

WriteLiteral(" data-bind=\"click: save\"");

WriteLiteral(">\r\n\t\t\t\t\t<span");

WriteLiteral(" class=\"btn-label\"");

WriteLiteral(">\r\n\t\t\t\t\t\t<i");

WriteLiteral(" class=\"glyphicon glyphicon-ok\"");

WriteLiteral("></i>\r\n\t\t\t\t\t</span>");

            
            #line 20 "..\..\Areas\Manager\Views\PageTypeMgr\Edit.cshtml"
                      Write(Piranha.Manager.Resources.Global.Save);

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t\t</button>\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t</div>\r\n");

            
            #line 25 "..\..\Areas\Manager\Views\PageTypeMgr\Edit.cshtml"


            
            #line default
            #line hidden
WriteLiteral("\t<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n\t\t<div");

WriteLiteral(" class=\"col-sm-12\"");

WriteLiteral(">\r\n\t\t\t<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n\t\t\t\t<input");

WriteLiteral(" name=\"Name\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control title\"");

WriteLiteral(" placeholder=\"Add name\"");

WriteLiteral(" data-bind=\"value: name\"");

WriteLiteral(" />\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t</div>\r\n");

WriteLiteral("\t<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n\t\t<div");

WriteLiteral(" class=\"col-sm-8\"");

WriteLiteral(">\r\n\t\t\t<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n\t\t\t\t<textarea");

WriteLiteral(" name=\"Description\"");

WriteLiteral(" class=\"form-control count-me\"");

WriteLiteral(" rows=\"4\"");

WriteLiteral(" placeholder=\"Add optional description\"");

WriteLiteral(" data-bind=\"value: description\"");

WriteLiteral("></textarea>\r\n\t\t\t\t<p><span");

WriteLiteral(" data-bind=\"text: descriptionLength\"");

WriteLiteral("></span>/255 characters</p>\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t\t<div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n\t\t\t<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n\t\t\t\t<input");

WriteLiteral(" name=\"Slug\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control meta\"");

WriteLiteral(" placeholder=\"Slug will be generated automatically\"");

WriteLiteral(" data-bind=\"value: slug\"");

WriteLiteral(" />\r\n\t\t\t</div>\r\n\t\t\t<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n\t\t\t\t<input");

WriteLiteral(" name=\"Route\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control meta\"");

WriteLiteral(" placeholder=\"Add optional route\"");

WriteLiteral(" data-bind=\"value: route\"");

WriteLiteral(" />\r\n\t\t\t</div>\r\n\t\t\t<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n\t\t\t\t<input");

WriteLiteral(" name=\"View\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control meta\"");

WriteLiteral(" placeholder=\"Add optional view\"");

WriteLiteral(" data-bind=\"value: view\"");

WriteLiteral(" />\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t</div>\r\n");

            
            #line 52 "..\..\Areas\Manager\Views\PageTypeMgr\Edit.cshtml"


            
            #line default
            #line hidden
WriteLiteral("\t<div");

WriteLiteral(" class=\"row with-sidebar\"");

WriteLiteral(">\r\n\t\t<div");

WriteLiteral(" class=\"col-sm-12\"");

WriteLiteral(">\t\r\n\t\t\t<!-- Regions -->\r\n\t\t\t<table");

WriteLiteral(" class=\"table table-striped\"");

WriteLiteral(">\r\n\t\t\t\t<thead>\r\n\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t<th>Name</th>\r\n\t\t\t\t\t\t<th>Internal id</th>\r\n\t\t\t\t\t" +
"\t<th>Collection</th>\r\n\t\t\t\t\t\t<th>Type</th>\r\n\t\t\t\t\t\t<th");

WriteLiteral(" class=\"actions three\"");

WriteLiteral("></th>\r\n\t\t\t\t\t</tr>\r\n\t\t\t\t</thead>\r\n\t\t\t\t<tbody>\r\n\t\t\t\t\t<!-- ko foreach: { data: regi" +
"ons, as: \'region\' } -->\r\n\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-bind=\"value: region.Name\"");

WriteLiteral(" />\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-bind=\"value: region.InternalId\"");

WriteLiteral(" />\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" data-bind=\"checked: region.IsCollection\"");

WriteLiteral(" />\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<select");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-bind=\"options: $root.regionTypes, optionsText: \'Text\', value: region.CLRTyp" +
"e\"");

WriteLiteral("></select>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td");

WriteLiteral(" class=\"actions\"");

WriteLiteral(">\r\n\t\t\t\t\t\t\t<a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"up\"");

WriteLiteral(" data-bind=\"click: function() { $root.regionUp(region); }\"");

WriteLiteral("></a>\r\n\t\t\t\t\t\t\t<a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"down\"");

WriteLiteral(" data-bind=\"click: function() { $root.regionDown(region); }\"");

WriteLiteral("></a>\r\n\t\t\t\t\t\t\t<a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"delete\"");

WriteLiteral(" data-bind=\"click: function() { $root.regionDelete(region); }\"");

WriteLiteral("></a>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t</tr>\r\n\t\t\t\t\t<!-- /ko -->\r\n\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t" +
"<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n\t\t\t\t\t\t\t\t<input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-bind=\"value: newRegionName\"");

WriteLiteral(" placeholder=\"Display name\"");

WriteLiteral(" />\r\n\t\t\t\t\t\t\t\t<!-- ko if: newRegionNameError -->\r\n\t\t\t\t\t\t\t\t<span");

WriteLiteral(" class=\"field-validation-error\"");

WriteLiteral(" data-bind=\"text: newRegionNameError\"");

WriteLiteral("></span>\r\n\t\t\t\t\t\t\t\t<!-- /ko -->\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<d" +
"iv");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n\t\t\t\t\t\t\t\t<input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-bind=\"value: newRegionId\"");

WriteLiteral(" placeholder=\"Internal id\"");

WriteLiteral(" />\r\n\t\t\t\t\t\t\t\t<!-- ko if: newRegionIdError -->\r\n\t\t\t\t\t\t\t\t<span");

WriteLiteral(" class=\"field-validation-error\"");

WriteLiteral(" data-bind=\"text: newRegionIdError\"");

WriteLiteral("></span>\r\n\t\t\t\t\t\t\t\t<!-- /ko -->\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<i" +
"nput");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" data-bind=\"checked: newRegionCollection\"");

WriteLiteral(" />\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n\t\t\t\t\t\t\t\t<select");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-bind=\"options: $root.regionTypes, optionsText: \'Text\', value: newRegionType" +
", optionsCaption: \'Choose type\'\"");

WriteLiteral("></select>\r\n\t\t\t\t\t\t\t\t<!-- ko if: newRegionTypeError -->\r\n\t\t\t\t\t\t\t\t<span");

WriteLiteral(" class=\"field-validation-error\"");

WriteLiteral(" data-bind=\"text: newRegionTypeError\"");

WriteLiteral("></span>\r\n\t\t\t\t\t\t\t\t<!-- /ko -->\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<b" +
"utton");

WriteLiteral(" class=\"btn btn-primary\"");

WriteLiteral(" data-bind=\"click: regionAdd\"");

WriteLiteral(">Add</button>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t</tr>\r\n\t\t\t\t</tbody>\r\n\t\t\t</table>\r\n\r\n\t\t\t<!-- Prop" +
"erties -->\r\n\t\t\t<table");

WriteLiteral(" class=\"table table-striped\"");

WriteLiteral(">\r\n\t\t\t\t<thead>\r\n\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t<th>Name</th>\r\n\t\t\t\t\t\t<th>Internal id</th>\r\n\t\t\t\t\t" +
"\t<th>Collection</th>\r\n\t\t\t\t\t\t<th>Type</th>\r\n\t\t\t\t\t\t<th");

WriteLiteral(" class=\"actions three\"");

WriteLiteral("></th>\r\n\t\t\t\t\t</tr>\r\n\t\t\t\t</thead>\r\n\t\t\t\t<tbody>\r\n\t\t\t\t\t<!-- ko foreach: { data: prop" +
"erties, as: \'property\' } -->\r\n\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-bind=\"value: property.Name\"");

WriteLiteral(" />\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-bind=\"value: property.InternalId\"");

WriteLiteral(" />\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" data-bind=\"checked: property.IsCollection\"");

WriteLiteral(" />\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<select");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-bind=\"options: $root.propertyTypes, optionsText: \'Text\', value: property.CL" +
"RType\"");

WriteLiteral("></select>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td");

WriteLiteral(" class=\"actions\"");

WriteLiteral(">\r\n\t\t\t\t\t\t\t<a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"up\"");

WriteLiteral(" data-bind=\"click: function() { $root.propertyUp(property); }\"");

WriteLiteral("></a>\r\n\t\t\t\t\t\t\t<a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"down\"");

WriteLiteral(" data-bind=\"click: function() { $root.propertyDown(property); }\"");

WriteLiteral("></a>\r\n\t\t\t\t\t\t\t<a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"delete\"");

WriteLiteral(" data-bind=\"click: function() { $root.propertyDelete(property); }\"");

WriteLiteral("></a>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t</tr>\r\n\t\t\t\t\t<!-- /ko -->\r\n\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t" +
"<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n\t\t\t\t\t\t\t\t<input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-bind=\"value: newPropertyName\"");

WriteLiteral(" placeholder=\"Display name\"");

WriteLiteral(" />\r\n\t\t\t\t\t\t\t\t<!-- ko if: newPropertyNameError -->\r\n\t\t\t\t\t\t\t\t<span");

WriteLiteral(" class=\"field-validation-error\"");

WriteLiteral(" data-bind=\"text: newPropertyNameError\"");

WriteLiteral("></span>\r\n\t\t\t\t\t\t\t\t<!-- /ko -->\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<d" +
"iv");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n\t\t\t\t\t\t\t\t<input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-bind=\"value: newPropertyId\"");

WriteLiteral(" placeholder=\"Internal id\"");

WriteLiteral(" />\r\n\t\t\t\t\t\t\t\t<!-- ko if: newPropertyIdError -->\r\n\t\t\t\t\t\t\t\t<span");

WriteLiteral(" class=\"field-validation-error\"");

WriteLiteral(" data-bind=\"text: newPropertyIdError\"");

WriteLiteral("></span>\r\n\t\t\t\t\t\t\t\t<!-- /ko -->\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<i" +
"nput");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" data-bind=\"checked: newPropertyCollection\"");

WriteLiteral(" />\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n\t\t\t\t\t\t\t\t<select");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" data-bind=\"options: $root.propertyTypes, optionsText: \'Text\', value: newProperty" +
"Type, optionsCaption: \'Choose type\'\"");

WriteLiteral("></select>\r\n\t\t\t\t\t\t\t\t<!-- ko if: newPropertyTypeError -->\r\n\t\t\t\t\t\t\t\t<span");

WriteLiteral(" class=\"field-validation-error\"");

WriteLiteral(" data-bind=\"text: newPropertyTypeError\"");

WriteLiteral("></span>\r\n\t\t\t\t\t\t\t\t<!-- /ko -->\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t<b" +
"utton");

WriteLiteral(" class=\"btn btn-primary\"");

WriteLiteral(" data-bind=\"click: propertyAdd\"");

WriteLiteral(">Add</button>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t</tr>\r\n\t\t\t\t</tbody>\r\n\t\t\t</table>\r\n\t\t</div>\r\n\t</d" +
"iv>\r\n");

            
            #line 192 "..\..\Areas\Manager\Views\PageTypeMgr\Edit.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
