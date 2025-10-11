using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SciTrack.Api.Migrations
{
    /// <inheritdoc />
    public partial class KhoiTaoDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HopDongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHopDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDoiTac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayHieuLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayNghiemThu = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TongGiaTri = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ChiPhiTuKetQuaDeTai = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ChiPhiTuTrangThietBi = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ChiPhiChuyenMon = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoiNhuan = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KetQuaDeTais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSoKetQua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenKetQua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhanLoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DinhGia = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GiaTriConLai = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CacHopDongSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KetQuaDeTais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrangThietBis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaThietBi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenThietBi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySuDung = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguyenGia = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    KhauHaoHaoMon = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GiaTriConLai = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LienQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhatKySuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinhTrang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThietBis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeTais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSoDeTai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDeTai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhatTaiSan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QuyetDinhLienQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuyetDinhXuLyTaiSan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KinhPhiThucHien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    KinhPhiGiaoKhoan = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    KinhPhiVatTu = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HaoMonKhauHaoLienQuan = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    KetQuaDeTaiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeTais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeTais_KetQuaDeTais_KetQuaDeTaiId",
                        column: x => x.KetQuaDeTaiId,
                        principalTable: "KetQuaDeTais",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaiSans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguyenGia = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    KhauHao = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HaoMon = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GiaTriConLai = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeTaiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiSans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaiSans_DeTais_DeTaiId",
                        column: x => x.DeTaiId,
                        principalTable: "DeTais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeTais_KetQuaDeTaiId",
                table: "DeTais",
                column: "KetQuaDeTaiId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiSans_DeTaiId",
                table: "TaiSans",
                column: "DeTaiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HopDongs");

            migrationBuilder.DropTable(
                name: "TaiSans");

            migrationBuilder.DropTable(
                name: "TrangThietBis");

            migrationBuilder.DropTable(
                name: "DeTais");

            migrationBuilder.DropTable(
                name: "KetQuaDeTais");
        }
    }
}
