<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditClient.aspx.cs" Inherits="SkripsiIanKeefe.EditClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Supplier</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Edit Supplier</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Client No</li>
                        <asp:Label ID="lblclientno" runat="server" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Client Nama</li>
                        <asp:TextBox runat="server" ID="txtboxclientname" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Submit" CssClass="btn btn-primary" runat="server" ID="lbadd" OnClick="lbadd_Click" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <asp:LinkButton Text="Cancel" CssClass="btn btn-primary" runat="server" ID="lbcancel" OnClick="lbcancel_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
</asp:Content>
