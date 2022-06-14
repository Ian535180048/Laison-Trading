<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DetailPenjualanAdmin.aspx.cs" Inherits="SkripsiIanKeefe.DetailPenjualanAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Penjualan</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Penjualan Paging</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Transaksi No</li>
                        <asp:Label ID="lbltransno" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Nama Customer</li>
                        <asp:Label ID="lblnamacust" runat="server" />
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Username</li>
                        <asp:Label ID="lblusername" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Alamat</li>
                        <asp:Label ID="lblalamat" runat="server" />
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Ongkir Nama</li>
                        <asp:Label ID="lblongkirnama" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Ongkir Type</li>
                        <asp:Label ID="lblongkirtype" runat="server" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Total Harga</li>
                        <asp:Label ID="lbltotalharga" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Total Ongkir</li>
                        <asp:Label ID="lbltotalongkir" runat="server" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Status Penjualan</li>
                        <asp:Label ID="lblstatus" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Tanggal Transaksi</li>
                        <asp:Label ID="lbltanggal" runat="server" />
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Tanggal Terbayar</li>
                        <asp:Label ID="lbltanggalterbayar" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Tanggal Diterima</li>
                        <asp:Label ID="lbltanggalterima" runat="server" />
                    </div>
                </div>
            </div>

            <asp:UpdatePanel runat="server" ID="upresi" Visible="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <div class="form-floating mb-3 mb-md-0">
                                <li class="breadcrumb-item active">Resi Pengiriman</li>
                                <asp:HyperLink ID="hype_resi" Text="Lihat Resi Pengiriman" runat="server" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <asp:UpdatePanel runat="server" ID="upbukti" Visible="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <div class="form-floating mb-3 mb-md-0">
                                <li class="breadcrumb-item active">Bukti Pembayaran</li>
                                <asp:HyperLink ID="hype_bukti" Text="Lihat Bukti Pembayaran" runat="server" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel runat="server" ID="upreason" Visible="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <div class="form-floating mb-3 mb-md-0">
                                <li class="breadcrumb-item active">Notes</li>
                                <asp:Label ID="lblnotes" runat="server" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Phone Number</li>
                        <asp:Label ID="lblphone" runat="server" />
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
                            <asp:Literal Text="Jumlah Barang" ID="ltlstock" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_stok" Text='<%# Eval("JUMLAH_BARANG") %>' runat="server" />
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
                            <asp:Label ID="lb_harga" Text='<%# Eval("TOTAL", "{0:Rp 0,00}") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
