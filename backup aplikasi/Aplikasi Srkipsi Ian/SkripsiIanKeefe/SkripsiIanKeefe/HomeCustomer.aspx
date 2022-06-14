<%@ Page Title="" Language="C#" MasterPageFile="~/MainCustomer.Master" AutoEventWireup="true" CodeBehind="HomeCustomer.aspx.cs" Inherits="SkripsiIanKeefe.HomeCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header class="bg-dark py-5">
        <div class="container px-4 px-lg-5 my-5">
            <div class="text-center text-white">
                <h1 class="display-4 fw-bolder">Laison</h1>
                <p class="lead fw-normal text-white-50 mb-0">Selamat Datang Di Laison</p>
            </div>
        </div>
    </header>

    <asp:ScriptManager runat="server" ID="smApplication" />

    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Nama Produk</li>
                        <asp:TextBox ID="txtboxnamaproduk" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Minimal Harga Produk</li>
                        <asp:TextBox runat="server" ID="txtboxharga_mulai" OnTextChanged="txtboxharga_mulai_TextChanged" AutoPostBack="true" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="Number Only" ControlToValidate="txtboxharga_mulai" ValidationExpression="[0-9]+(,[0-9]+)*" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Maksimal Harga Produk</li>
                        <asp:TextBox runat="server" ID="txtboxhargaakhir" OnTextChanged="txtboxhargaakhir_TextChanged" AutoPostBack="true" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ErrorMessage="Number Only" ControlToValidate="txtboxhargaakhir" ValidationExpression="[0-9]+(,[0-9]+)*" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <br />
                        <asp:LinkButton Text="Search" CssClass="btn btn-primary" runat="server" ID="ltlsearch" OnClick="ltlsearch_Click" />
                    </div>
                </div>
            </div>
            <br />
        </div>
    </div>
    <br />
    <asp:Label Text="Stok Habis" ID="lblerror" ForeColor="Red" Visible="false" runat="server" />
    <section class="py-5">
        <div class="container px-4 px-lg-5 mt-5">
            <div class="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">

                <asp:ListView runat="server" ID="dlproduk" OnItemCommand="dlproduk_ItemCommand1">
                    <ItemTemplate>
                        <div class="col mb-5">
                            <div class="card h-100">
                                <asp:Image CssClass="card-img-top" ImageUrl='<%# Eval("FOTO") %>' runat="server" />
                                <div class="card-body p-4">
                                    <div class="text-center">
                                        <asp:Label CssClass="d-flex justify-content-center" ID="lbl_prod_id" Text='<%# Eval("PROD_ID") %>' runat="server" Style="font-family: Verdana; font-size: 20px;" Visible="false" />
                                        <asp:Label CssClass="d-flex justify-content-center" ID="lbl_prodname" Text='<%# Eval("PROD_NAME") %>' runat="server" Style="font-family: Verdana; font-size: 20px;" />
                                        <asp:Label CssClass="d-flex justify-content-center" ID="lblprice" Text='<%# Eval("PRICE", "{0:Rp 0,00}") %>' runat="server" Style="font-weight: bold;" />
                                    </div>
                                </div>
                                <div class="card-footer p-4 pt-0 border-top-0 bg-transparent">
                                    <div class="text-center">
                                        <asp:Button CssClass="btn btn-outline-dark mt-auto" ID="lbdetail" CommandName="VIEW" Text="Detail" runat="server" CausesValidation="false" CommandArgument='<%# Eval("PROD_ID") %>' />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </section>
</asp:Content>
