<%@ Page Title="" Language="C#" MasterPageFile="~/MainCustomer.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="SkripsiIanKeefe.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Cart</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">List Cart</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Produk Nama</li>
                        <asp:TextBox runat="server" ID="txtboxprodnama" />
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Search" CssClass="btn btn-primary" runat="server" ID="ltlsearch" OnClick="ltlsearch_Click" />
                    </div>
                </div>
            </div>
            <br />
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="uperror" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label Text="Produk yang dibeli melibihi stok" ID="lblerrorstok" ForeColor="Red" Visible="false" runat="server" />
            <asp:Label Text="Mohon piih salah satu produk yang terdapat keranjang anda" ID="lblerrorcart" ForeColor="Red" Visible="false" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <br />
    <asp:UpdatePanel runat="server" ID="upcart" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView runat="server" ID="gvcart" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped" OnRowCommand="gvcart_RowCommand">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:CheckBox ID="chckbox" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Image" ID="ltlimage" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblcartid" Text='<%# Eval("CART_ID") %>' runat="server" Visible="false" />
                            <asp:Label ID="lblprodid" Text='<%# Eval("PROD_ID") %>' runat="server" Visible="false" />
                            <asp:Image ID="imgcart" ImageUrl='<%# Eval("FOTO") %>' runat="server" Style="width: 380px; height: 380px;" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Nama Produk" ID="ltlprodname" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_prod_name" Text='<%# Eval("PROD_NAME") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Nomor Produk" ID="ltlprodno" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_prod_no" Text='<%# Eval("PROD_NO") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Ammount" ID="ltlammount" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbL_amount" Text='<%# Eval("AMMOUNT") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Harga" ID="ltltotalharga" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_harga" Text='<%# Eval("PRICE", "{0:0,00}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Total Harga" ID="ltltotalharga" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbltotalharga" Text='<%# Eval("TOTAL", "{0:0,00}") %>' runat="server" />
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
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Total Harga</li>
                        <asp:Label ID="lbltotalharga" runat="server" />
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Checkout" CssClass="btn btn-primary" runat="server" ID="lbcheckout" OnClick="lbcheckout_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />



</asp:Content>
