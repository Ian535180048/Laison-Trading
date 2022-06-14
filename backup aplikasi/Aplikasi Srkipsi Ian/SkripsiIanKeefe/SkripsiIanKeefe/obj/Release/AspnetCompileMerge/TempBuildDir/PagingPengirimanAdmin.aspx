<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PagingPengirimanAdmin.aspx.cs" Inherits="SkripsiIanKeefe.PagingPengirimanAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Daftar Penjualan</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Pengiriman Produk</li>
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
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Nama Penerima</li>
                        <asp:TextBox runat="server" ID="txtboxnamacustomer" />
                    </div>
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
                    <asp:LinkButton Text="Search" CssClass="btn btn-primary" runat="server" ID="ltlsearch" OnClick="ltlsearch_Click"/>
                </div>
            </div>

        </div>
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
                            <asp:Literal Text="Nomor Transaksi" ID="ltltransno" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server" CausesValidation="false" CommandName="DTL" />
                            <asp:Label ID="lbl_trans_id" Text='<%# Eval("TRANS_PENJUALAN_ID") %>' runat="server" Visible="false" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Nama Pelanggan" ID="ltlcustfirstname" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_Cust_first_Name" Text='<%# Eval("NAMA_PENERIMA") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Alamat" ID="ltlalamat" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_alamat" Text='<%# Eval("ALAMAT") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Nama Ongkir" ID="ltlnamaongkir" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_ongkir" Text='<%# Eval("ONGKIR_NAMA") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Tipe Ongkir" ID="ltltipe" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_tipe" Text='<%# Eval("ONGKIR_TYPE") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Total Harga" ID="ltlhargaproduk" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_total" Text='<%# Eval("TOTAL", "{0:Rp 0,00}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Pengiriman" ID="ltlapproval" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Image/Pembayaran.png" CommandName="APV" runat="server" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
