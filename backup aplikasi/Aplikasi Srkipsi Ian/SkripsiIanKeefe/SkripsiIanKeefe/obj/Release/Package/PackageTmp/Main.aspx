<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="SkripsiIanKeefe.Main1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="mt-4">Welcome To Laison Trading</h1>
    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Home Page</li>
     </ol>

    <asp:Literal Text=" Welcome, This Is The Admin Home Page" ID="ltl_admin" runat="server" />
</asp:Content>
