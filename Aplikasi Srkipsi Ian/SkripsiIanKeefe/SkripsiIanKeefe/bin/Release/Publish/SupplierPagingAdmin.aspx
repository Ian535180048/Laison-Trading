<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SupplierPagingAdmin.aspx.cs" Inherits="SkripsiIanKeefe.SupplierPagingAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Pemasok</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Daftar Pemasok</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Nama Pemasok</li>
                        <asp:TextBox runat="server" ID="txtboxsuppliername" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Nomor Pemasok</li>
                        <asp:TextBox runat="server" ID="txtboxsupplierno" />
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
                <asp:Label ID="lblerrordelete1" Text="Data Supplier Tidak Dapat Dihapus Dikarenakan Terdapat Transaksi Pembelian Stok Barang Pada Supplier Ini" Visible="false" runat="server" ForeColor="Red" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <asp:UpdatePanel runat="server" ID="upcust" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView runat="server" ID="gvsupplier" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped" OnRowCommand="gvsupplier_RowCommand">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Supplier Name" ID="ltlsuppliername" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_supplier_name" Text='<%# Eval("SUPPLIER_NAME") %>' runat="server" />
                            <asp:Label ID="lbl_supplier_id" Text='<%# Eval("SUPPLIER_ID") %>' runat="server" Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Supplier No" ID="ltlsupplierno" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_supplier_no" Text='<%# Eval("SUPPLIER_NO") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Edit" ID="ltledit" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Image/edit.png" CommandName="ED" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Delete" ID="ltldelete" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton2" ImageUrl="~/Image/trash.png" CommandName="DEL" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
