<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PrediksiStokBarang.aspx.cs" Inherits="SkripsiIanKeefe.PrediksiStokBarang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Prediksi Stok Barang</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Prediksi Stok Barang </li>
    </ol>
    <br />

    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Metode yang digunakan</li>
                        <asp:DropDownList runat="server" AutoPostBack="true" ID="dlmetode" OnSelectedIndexChanged="dlmetode_SelectedIndexChanged">
                            <asp:ListItem Text="Double Exponential Smoothing" Value="DS" />
                            <asp:ListItem Text="Least Square" Value="LS" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <asp:UpdatePanel ID="updatatahunds" runat="server" Visible="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <div class="form-floating mb-3 mb-md-0">
                                <li class="breadcrumb-item active">Prediksi untuk berapa bulan :</li>
                                <asp:DropDownList runat="server" ID="dlbulan">
                                    <asp:ListItem Text="1" Value="1" />
                                    <asp:ListItem Text="2" Value="2" />
                                    <asp:ListItem Text="3" Value="3" />
                                    <asp:ListItem Text="4" Value="4" />
                                    <asp:ListItem Text="5" Value="5" />
                                    <asp:ListItem Text="6" Value="6" />
                                    <asp:ListItem Text="7" Value="7" />
                                    <asp:ListItem Text="8" Value="8" />
                                    <asp:ListItem Text="9" Value="9" />
                                    <asp:ListItem Text="10" Value="10" />
                                    <asp:ListItem Text="11" Value="11" />
                                    <asp:ListItem Text="12" Value="12" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-floating">
                                <li class="breadcrumb-item active">Produk yang diprediksi :</li>
                                <asp:DropDownList ID="dlproduk" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <div class="form-floating mb-3 mb-md-0">
                                <li class="breadcrumb-item active">Estimasi Harga Pembelian Per Produk :</li>
                                <asp:TextBox runat="server" ID="txtboxpemesananprodukds" />
                                <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxpemesananprodukds" runat="server" ForeColor="Red" />
                                <asp:RegularExpressionValidator ErrorMessage="Number Only" ControlToValidate="txtboxpemesananprodukds" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
                                <asp:RangeValidator ErrorMessage="Harus Lebih dari 0" MinimumValue="1" ControlToValidate="txtboxpemesananprodukds" runat="server" Type="Integer" MaximumValue="100000000" ForeColor="Red" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-floating">
                                <li class="breadcrumb-item active">Estimasi Harga Pengiriman Produk Dari Produsen :</li>
                                <asp:TextBox runat="server" ID="txtboxpengirimanprodukds" />
                                <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxpengirimanprodukds" runat="server" ForeColor="Red" />
                                <asp:RegularExpressionValidator ErrorMessage="Number Only" ControlToValidate="txtboxpengirimanprodukds" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>



            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Hitung" CssClass="btn btn-primary" ID="lbcalculate" OnClick="lbcalculate_Click" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />

    <asp:UpdatePanel runat="server" ID="updatapastls" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label CssClass="breadcrumb-item active" ID="lbldatals" Text="Hasil Aktual" Visible="false" runat="server" />
            <br />
            <asp:GridView runat="server" ID="gvdatapastls" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped table-responsive">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Tahun" ID="ltltahun" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_year" Text='<%# Eval("year") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Bulan" ID="ltlbulan" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_bulan" Text='<%# Eval("month") %>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbltotal" Text="Total" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Data Penjualan (y)" ID="ltly" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_y" Text='<%# Eval("count") %>' runat="server" />
                            <asp:Label ID="lbl_xy" Text='<%# Eval("xy") %>' runat="server" Visible="false" />
                            <asp:Label ID="lbl_x2" Text='<%# Eval("xpangkat") %>' runat="server" Visible="false" />
                            <asp:Label ID="lbl_x" Text='<%# Eval("x") %>' runat="server" Visible="false" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Chart ID="chartdataaktualls" runat="server">
                <Series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="chartarea1">
                        <AxisX Title="Bulan"></AxisX>
                        <AxisY Title="Data Penjualan"></AxisY>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:Label ID="lblgrafikpenjualanls" runat="server" Visible="false" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="uptotalls" Visible="false">
        <ContentTemplate>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Total Penjualan</li>
                        <asp:Label ID="lbltotalpenjualan" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <asp:Label ID="lbltotalx" runat="server" Visible="false" />
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:Label ID="lbltotalxkuadrat" runat="server" Visible="false" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <asp:Label ID="lbltotalxy" runat="server" Visible="false" />
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:Label ID="lblnilaitrend" runat="server" Visible="false" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <asp:Label ID="lblnilairataratatrend" runat="server" Visible="false" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server" ID="upleast" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label CssClass="breadcrumb-item active" ID="lblhasills" Text="Hasil Data Prediksi Least Square & EOQ" Visible="false" runat="server" />
            <br />
            <asp:GridView runat="server" ID="gvleast" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped table-responsive">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Tahun" ID="ltltahun1" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_year1" Text='<%# Eval("tahun") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Bulan" ID="ltlbulan1" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_bulan1" Text='<%# Eval("bulan") %>' runat="server" />
                            <asp:Label ID="lbl_x1" Text='<%# Eval("x") %>' runat="server" Visible="false" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbltotal" Text="Total" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Prediksi Penjualan" ID="ltlprediksi" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_prediksi" Text='<%# Eval("prediksi") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Jumlah Satuan Per Pesanan" ID="ltlpesanan" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_eoq" Text='<%# Eval("eoq") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Safety Stock" ID="ltlsafety" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_safety" Text='<%# Eval("safety") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Maximum Inventory" ID="ltlmax" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_max" Text='<%# Eval("max") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

            <asp:Chart ID="charthasills" runat="server">
                <Series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea>
                        <AxisX Title="Bulan"></AxisX>
                        <AxisY Title="Prediksi Penjualan"></AxisY>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:Label ID="lblhasillsgrafik" Text="Grafik Hasil Prediksi Penjualan Least Square" runat="server" Visible="false"/>
        </ContentTemplate>
    </asp:UpdatePanel>


    <!-- Least Square -->

    <!-- Double Exponential -->
    <asp:UpdatePanel runat="server" ID="updataaktualds" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label CssClass="breadcrumb-item active" ID="lblaktualds" Text="Hasil Aktual" Visible="false" runat="server" />
            <br />
            <asp:GridView runat="server" ID="gvdataaktualds" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped table-responsive">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Tahun" ID="ltltahun" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_year" Text='<%# Eval("year") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Bulan" ID="ltlbulan" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_bulan" Text='<%# Eval("month") %>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbltotal" Text="Total" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Data Aktual Penjualan" ID="ltlpenj" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_penj" Text='<%# Eval("count") %>' runat="server" />
                            <asp:Label ID="lbl_tunggal" Text='<%# Eval("tunggal", "{0:0.00}") %>' runat="server" Visible="false" />
                            <asp:Label ID="lbl_ganda" Text='<%# Eval("ganda", "{0:0.00}") %>' runat="server" Visible="false" />
                            <asp:Label ID="lbl_at" Text='<%# Eval("at", "{0:0.00}") %>' runat="server" Visible="false" />
                            <asp:Label ID="lbl_slope" Text='<%# Eval("bt", "{0:0.00}") %>' runat="server" Visible="false" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" Visible="false">
                        <HeaderTemplate>
                            <asp:Literal Text="Niai Peramalan Untuk Data Aktual" ID="ltlperamalan" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_ft" Text='<%# Eval("ft") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" Visible="false">
                        <HeaderTemplate>
                            <asp:Literal Text="Persentase Error" ID="ltlmean" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_p" Text='<%# Eval("p", "{0:0.00}") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

            <asp:Chart ID="chartdataaktualds" runat="server">
                <Series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="chartarea1">
                        <AxisX Title="Bulan"></AxisX>
                        <AxisY Title="Data Penjualan"></AxisY>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:Label ID="lblgrafikpenjualands" runat="server" Visible="false"/>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="upmeanerror" Visible="false" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Nilai Mean Absolute Error</li>
                        <asp:Label ID="lblmeanabsolute" runat="server" />
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="upjumlahds" UpdateMode="Conditional" Visible="false">
        <ContentTemplate>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Total Penjualan</li>
                        <asp:Label ID="lbljumlahdatads" runat="server" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server" ID="updatahasilds" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label CssClass="breadcrumb-item active" ID="lblhasilds" Text="Hasil Data Prediksi" Visible="false" runat="server" />
            <br />
            <asp:GridView runat="server" ID="gvhasilds" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped table-responsive">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Tahun" ID="ltltahun" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_year" Text='<%# Eval("year") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Bulan" ID="ltlbulan" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_bulan" Text='<%# Eval("month") %>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbltotal" Text="Total" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Prediksi Penjualan" ID="ltlpenj" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_penj" Text='<%# Eval("prediksi") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Jumlah Satuan Per Pesanan" ID="ltlpesanan" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_eoq" Text='<%# Eval("eoq", "{0:0.00}") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Safety Stock" ID="ltlsafety" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_safety" Text='<%# Eval("safety") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Maximum Inventory" ID="ltlmax" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_max" Text='<%# Eval("max") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Chart ID="charthasilds" runat="server">
                <Series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="chartarea1">
                        <AxisX Title="Bulan"></AxisX>
                        <AxisY Title="Prediksi Penjualan"></AxisY>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:Label ID="lblhasilgrafikds" Text="Grafik Hasil Prediksi Penjualan Double Exponential Smoothing" runat="server" Visible="false"/>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Double Exponential -->
</asp:Content>
