<%@ Page Title="" Language="C#" MasterPageFile="~/MainCustomer.Master" AutoEventWireup="true" CodeBehind="CheckoutPembayaran.aspx.cs" Inherits="SkripsiIanKeefe.CheckoutPembayaran" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Checkout</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Checkout Paging</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Total Harga Produk</li>
                        <asp:Label ID="lbltotalharga" runat="server" />
                        <asp:Label ID="lblberatproduk" runat="server" Visible="false" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Alamat</li>
                        <asp:TextBox runat="server" ID="txtboxalamat" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Required" ControlToValidate="txtboxalamat" ForeColor="Red" runat="server" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Nama Penerima</li>
                    <asp:TextBox runat="server" ID="txtboxnamapenerima" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Required" ControlToValidate="txtboxnamapenerima" ForeColor="Red" runat="server" />
                </div>
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Tujuan Kota</li>
                    <asp:DropDownList runat="server" ID="dlltujuan" OnSelectedIndexChanged="dlltujuan_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Kurir</li>
                    <asp:DropDownList runat="server" ID="ddlkurir" OnSelectedIndexChanged="ddlkurir_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="JNE" Value="jne"></asp:ListItem>
                        <asp:ListItem Text="TIKI" Value="tiki"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Tipe Ongkir</li>
                    <asp:DropDownList runat="server" ID="ddltipeongkir" OnSelectedIndexChanged="ddltipeongkir_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Harga Ongkir</li>
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="uphargaongkir">
                        <ContentTemplate>
                             <asp:Label ID="lblhargaongkir" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Phone Number</li>
                    <asp:TextBox ID="txtboxphonenumber" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Required" ControlToValidate="txtboxphonenumber" ForeColor="Red" runat="server" />
                    <asp:RegularExpressionValidator ErrorMessage="Format Nomor Telpon Salah"
                                                                ValidationExpression="^(\+62|62|0)8[1-9][0-9]{6,9}$" 
                                                                ForeColor="Red" ControlToValidate="txtboxphonenumber" runat="server" />
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
    <asp:UpdatePanel runat="server" ID="upcart" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView runat="server" ID="gvpembayaran" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Nama Produk" ID="ltlprodname" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblcartid" Text='<%# Eval("CART_ID") %>' runat="server" Visible="false" />
                            <asp:Label ID="lblpembayaranid" Text='<%# Eval("PEMBAYARAN_ID") %>' runat="server" Visible="false" />
                            <asp:Label ID="lblprodid" Text='<%# Eval("PROD_ID") %>' runat="server" Visible="false" />
                            <asp:Label ID="lblprodname" Text='<%# Eval("PROD_NAME") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="No Produk" ID="ltlprodno" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_prod_no" Text='<%# Eval("PROD_NO") %>' runat="server" />
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

                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
