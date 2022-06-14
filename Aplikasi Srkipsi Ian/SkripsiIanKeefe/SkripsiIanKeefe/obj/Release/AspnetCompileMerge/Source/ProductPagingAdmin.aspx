<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ProductPagingAdmin.aspx.cs" Inherits="SkripsiIanKeefe.ProductPagingAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Product</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Product Paging</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Product Nama</li>
                        <asp:TextBox runat="server" ID="txtboxproductname" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Product No</li>
                        <asp:TextBox runat="server" ID="txtboxproductno" />
                    </div>
                </div>
            </div>

            <br />
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Search" CssClass="btn btn-primary" runat="server" ID="ltlsearch" OnClick="ltlsearch_Click" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Add" CssClass="btn btn-primary" runat="server" ID="ltladd" OnClick="ltladd_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel runat="server" ID="uperror" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lblerrordelete1" Text="Product Tidak Dapat Dihapus Dikarenakan Terdapat Penjualan Dalam Produk Ini" Visible="false" runat="server" ForeColor="Red" />
                <asp:Label ID="lblerrordelete2" Text="Product Tidak Dapat Dihapus Dikarenakan Terdapat Transaksi Pembelian Stok Barang Dalam Produk Ini" Visible="false" runat="server" ForeColor="Red" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    <br />
    <asp:UpdatePanel runat="server" ID="upcust" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView runat="server" ID="gvproduct" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped" OnRowCommand="gvproduct_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Produk Nama" ID="ltlprodname" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbl_prod_name" Text='<%# Eval("PROD_NAME") %>' runat="server" CausesValidation="false" CommandName="DTL" />
                            <asp:Label ID="lbl_prod_id" Text='<%# Eval("PROD_ID") %>' runat="server" Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Prod No" ID="ltlprodno" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_prod_no" Text='<%# Eval("PROD_NO") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Jumlah Stok" ID="ltlstock" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_stok" Text='<%# Eval("STOCK_TOTAL") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Berat Produk" ID="ltlberat" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_berat" Text='<%# Eval("BERAT_PRODUK") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Harga Produk" ID="ltlharga" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_harga" Text='<%# Eval("PRICE", "{0:0,00}") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Is Active" ID="ltlactive" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_active" Text='<%# Eval("ACTIVE") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Action Aktif" ID="ltledit" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton" ImageUrl="~/Image/edit.png" CommandName="ACT" runat="server" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Edit" ID="ltledit" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Image/edit.png" CommandName="ED" runat="server" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Delete" ID="ltldelete" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton2" ImageUrl="~/Image/trash.png" CommandName="DEL" runat="server" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
