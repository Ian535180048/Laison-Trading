<%@ Page Title="" Language="C#" MasterPageFile="~/MainCustomer.Master" AutoEventWireup="true" CodeBehind="DetailProdCust.aspx.cs" Inherits="SkripsiIanKeefe.DetailProdCust" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Produk</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Detail Produk</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:Image ID="image1" runat="server" Style="width: 200px; height: 200px;" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Nama Produk :
                            <asp:Label ID="lblproduk" runat="server" Style="font-weight: bold;" /></li>
                        <br />
                        <li class="breadcrumb-item active">Harga Produk :
                            <asp:Label ID="lblprice" runat="server" Style="font-weight: bold;" /></li>
                        <br />
                        <li class="breadcrumb-item active">Berat Produk :
                            <asp:Label ID="lblberat" runat="server" Style="font-weight: bold;" /></li>
                        <br />
                        <li class="breadcrumb-item active">Produk Tersisa:
                            <asp:Label ID="lblamt" runat="server" Style="font-weight: bold;" /></li>
                        <br />
                        <li class="breadcrumb-item active">Banyaknya Produk Yang Akan Dibeli :</li>
                        <asp:TextBox runat="server" ID="txtboxammount" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Required" ControlToValidate="txtboxammount" runat="server" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="Number Only" ControlToValidate="txtboxammount" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>
            <asp:UpdatePanel ID="uperror" Visible="false" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <div class="form-floating mb-3 mb-md-0">
                                <asp:Label Text="Produk yang dibeli melebihi kapasitas" runat="server" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Submit" CssClass="btn btn-success" runat="server" ID="lbadd" OnClick="lbadd_Click" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <asp:LinkButton Text="Cancel" CssClass="btn btn-primary" runat="server" ID="lbcancel" OnClick="lbcancel_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="form-floating">
                    <li class="breadcrumb-item active">Deskripsi Produk :</li>
                    <asp:Label ID="lbldescr" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <br />
</asp:Content>
