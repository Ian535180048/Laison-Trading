<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddBiaya.aspx.cs" Inherits="SkripsiIanKeefe.AddBiaya" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Biaya</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Add Transaction Biaya</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Tipe Biaya</li>
                        <asp:DropDownList runat="server" ID="ddlbebantipe">
                            <asp:ListItem Text="Beban Listrik" Value="BL"></asp:ListItem>
                            <asp:ListItem Text="Beban Internet" Value="BI"></asp:ListItem>
                            <asp:ListItem Text="Beban Air" Value="BA"></asp:ListItem>
                            <asp:ListItem Text="Beban Pajak" Value="BP"></asp:ListItem>
                            <asp:ListItem Text="Beban Administrasi" Value="BAD"></asp:ListItem>
                            <asp:ListItem Text="Beban Lain-lain" Value="BDLL"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="ddlbebantipe" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Ammount</li>
                        <asp:TextBox runat="server" ID="txtboxammount" />
                        <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxammount" runat="server" ForeColor="Red" />
                        <asp:RegularExpressionValidator ErrorMessage="Number Only" ControlToValidate="txtboxammount" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Tanggal Transaksi</li>
                        <asp:TextBox ID="txtboxtanggal" TextMode="Date" runat="server" />
                        <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxtanggal" runat="server" ForeColor="Red" />
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
