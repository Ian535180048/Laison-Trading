<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PenjualanPagingAdmin.aspx.cs" Inherits="SkripsiIanKeefe.PenjualanPagingAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Penjualan</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Daftar Penjualan</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Nomor Transaksi</li>
                        <asp:TextBox runat="server" ID="txtboxtransno" />
                    </div>
                </div>
                <%--<div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Customer No</li>
                        <asp:TextBox runat="server" ID="txtboxcustno" />
                    </div>
                </div>--%>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Nama Penerima</li>
                    <asp:TextBox runat="server" ID="txtboxnamacustomer" />
                </div>
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Nama Produk</li>
                    <asp:TextBox runat="server" ID="txtboxproduk" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Tanggal Mulai</li>

                    <asp:TextBox runat="server" ID="txtboxtanggalawal" TextMode="Date" />
                    <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxtanggalawal" ForeColor="Red" runat="server" />
                </div>
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Tanggal Akhir</li>

                    <asp:TextBox runat="server" ID="txtboxtanggalakhir" TextMode="Date" />
                    <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxtanggalakhir" ForeColor="Red" runat="server" />
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Status</li>
                    <asp:DropDownList runat="server" ID="ddlstatus">
                        <asp:ListItem Text="Menunggu Pembayaran" Value="PD_REQ"></asp:ListItem>
                        <asp:ListItem Text="Menunggu Konfirmasi Pembayaran" Value="PD_APV_REQ"></asp:ListItem>
                        <asp:ListItem Text="Terbayar" Value="PAID"></asp:ListItem>
                        <asp:ListItem Text="Terkirim" Value="SENT"></asp:ListItem>
                        <asp:ListItem Text="Diterima" Value="FINISH"></asp:ListItem>
                        <asp:ListItem Text="Request Pembatalan" Value="CAN_REQ"></asp:ListItem>
                        <asp:ListItem Text="Dibatalkan" Value="CAN"></asp:ListItem>
                        <asp:ListItem Text="Menunggu Persetujuan Refund" Value="REQ_REFUND"></asp:ListItem>
                        <asp:ListItem Text="Semua" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

        </div>
    </div>
    <br />
    <div class="justify-content-end">
        <table>
            <tr>
                <td style="width: 100%">
                    <asp:LinkButton Text="Search" CssClass="btn btn-primary" runat="server" ID="ltlsearch" OnClick="ltlsearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:UpdatePanel runat="server" ID="upcust" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView runat="server" ID="gvpenjualan" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped" OnRowCommand="gvpenjualan_RowCommand">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Tanggal Transaksi" ID="ltltanggal" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_tanggal" Text='<%# Eval("TANGGAL", "{0: dd MMM yyyy}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Transaksi No" ID="ltltransno" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server" CausesValidation="false" CommandName="DTL" />
                            <asp:Label ID="lbl_trans_id" Text='<%# Eval("TRANS_PENJUALAN_ID") %>' runat="server" Visible="false" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Nama Penerima" ID="ltlcustfirstname" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_Cust_first_Name" Text='<%# Eval("NAMA_PENERIMA") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Nama Produk" ID="ltlnamaproduk" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_nama_produk" Text='<%# Eval("PROD_NAME") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Harga Produk" ID="ltlhargaproduk" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_harga_produk" Text='<%# Eval("HARGA_PRODUK", "{0:Rp 0,00}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Quantity Produk" ID="ltlquantityproduk" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_quantity_produk" Text='<%# Eval("JUMLAH_BARANG") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Status Penjualan" ID="ltlstatus" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_status" Text='<%# Eval("STATUS") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
