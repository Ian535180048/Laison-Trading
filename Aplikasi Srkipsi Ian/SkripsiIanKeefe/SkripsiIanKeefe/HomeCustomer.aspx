<%@ Page Title="" Language="C#" MasterPageFile="~/MainCustomer.Master" AutoEventWireup="true" CodeBehind="HomeCustomer.aspx.cs" Inherits="SkripsiIanKeefe.HomeCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Welcome </h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">List Produk</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Nama Produk</li>
                        <asp:TextBox runat="server" ID="txtboxnamaproduk" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Minimal Harga Produk</li>
                        <asp:TextBox runat="server" ID="txtboxharga_mulai" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="Number Only" ControlToValidate="txtboxharga_mulai" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Maksimal Harga Produk</li>
                        <asp:TextBox runat="server" ID="txtboxhargaakhir" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ErrorMessage="Number Only" ControlToValidate="txtboxhargaakhir" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
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
    <asp:Label Text="Stok Habis" ID="lblerror" ForeColor="Red" Visible="false" runat="server" />
    <section class="py-5">
        <div class="container px-4 px-lg-5 mt-5">
            <div class="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                <asp:ListView ID="dlproduk" OnItemCommand="dlproduk_ItemCommand1" runat="server">
                    <ItemTemplate>
                        <div class="col mb-5">
                            <div class="card h-100">
                                <%--<div class="card mb-5 mx-3" style="width: 20rem">
                                <div class="card-body">--%>
                                <asp:ImageButton CssClass="card-img-top" ImageUrl='<%# Eval("FOTO") %>' runat="server" CommandName="VIEW" CommandArgument='<%# Eval("PROD_ID") %>' />
                                <div class="card-body p-4">
                                    <div class="text-center">
                                        <asp:Label CssClass="d-flex justify-content-center" ID="lbl_prod_id" Text='<%# Eval("PROD_ID") %>' runat="server" Style="font-family: Verdana; font-size: 20px;" Visible="false" />
                                        <asp:Label CssClass="d-flex justify-content-center" ID="lbl_prodname" Text='<%# Eval("PROD_NAME") %>' runat="server" Style="font-family: Verdana; font-size: 20px;" />
                                        <%--<asp:Image CssClass="img-fluid img-thumbnail" ImageUrl='<%# Eval("FOTO") %>' runat="server" Style="width: 20em; height: 15em;" />--%>
                                        <asp:Label CssClass="d-flex justify-content-center" ID="lblprice" Text='<%# Eval("PRICE", "{0:Rp 0,00}") %>' runat="server" Style="font-weight: bold;" />
                                    </div>
                                </div>

                                <%--                                <tr>
                                    <td>
                                        <asp:Label CssClass="d-flex justify-content-center" ID="lbldescr" Text='<%# Eval("DESCR") %>' runat="server" />
                                    </td>
                                </tr>--%>
                                    <%--<td class="d-flex justify-content-center">
                                        <asp:LinkButton CssClass="btn btn-success mx-5" ID="btnview" Text="View" runat="server" CommandArgument='<%# Eval("PROD_ID") %>' CommandName="VIEW" />
                                    </td>--%>

                                <%--</div>--%>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>

            </div>
        </div>
    </section>


    <br />
</asp:Content>
