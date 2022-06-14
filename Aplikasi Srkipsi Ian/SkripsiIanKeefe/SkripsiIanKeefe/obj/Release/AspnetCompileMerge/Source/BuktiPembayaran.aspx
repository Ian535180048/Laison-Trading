<%@ Page Title="" Language="C#" MasterPageFile="~/MainCustomer.Master" AutoEventWireup="true" CodeBehind="BuktiPembayaran.aspx.cs" Inherits="SkripsiIanKeefe.BuktiPembayaran" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Pembayaran Paging</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Pembayaran Paging</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Mohon Upload Bukti Transaksi</li>
                        <asp:FileUpload ID="flpdbukti" runat="server" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="File yang diupload bukan .jpeg / .png /.jpg"
                            ControlToValidate="flpdbukti" ForeColor="Red"
                            ValidationExpression="(.*png$)|(.*jpg$)|(.*jpeg$)">
                        </asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <br />
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Submit" CssClass="btn btn-primary" runat="server" ID="ltladd" OnClick="ltladd_Click" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <asp:LinkButton Text="Cancel" CssClass="btn btn-primary" runat="server" ID="lbcancel" OnClick="lbcancel_Click"/>
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:UpdatePanel runat="server" ID="uperror" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label Text="Bukti pembayaran belum diupload" ID="lblerror" ForeColor="Red" Visible="false" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                     
                        </div>
                    </div>
                </div>
        </div>
    </div>
    <br />
    <asp:UpdatePanel runat="server" ID="upcust" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView runat="server" ID="gvproduct" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Produk Nama" ID="ltlprodname" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_prod_name" Text='<%# Eval("PROD_NAME") %>' runat="server" />
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
                            <asp:Literal Text="Ammount" ID="ltlstock" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_stok" Text='<%# Eval("JUMLAH_BARANG") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Harga" ID="ltlharga" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_harga" Text='<%# Eval("TOTAL_HARGA", "{0:0,00}") %>' runat="server"  />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel runat="server" ID="upharga" UpdateMode="Conditional">
        <ContentTemplate>
            <li class="breadcrumb-item active">Total Transaksi</li>
            <asp:Label id="lbltotalharga" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
