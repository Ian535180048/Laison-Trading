use laison

SELECT DISTINCT tp.* FROM TRANS_PENJUALAN TP 
JOIN CHECKOUT C ON TP.TRANS_PENJUALAN_ID = C.TRANS_PENJUALAN_ID 
WHERE TANGGAL >= '2021-11-01'

SELECT * FROM CHECKOUT





DELETE FROM TRANS_PENJUALAN
WHERE STATUS IS NULL

SELECT * FROM PRODUCT

UPDATE TRANS_PENJUALAN 
SET TOTAL  = 2075000
WHERE TRANS_PENJUALAN_ID = 20277

SELECT * FROM CHECKOUT


-----BUAT GET DATA PENJUALAN---

SELECT TP.TANGGAL, TP.NAMA_PENERIMA, P.PROD_NAME, C.HARGA_PRODUK, C.JUMLAH_BARANG
FROM TRANS_PENJUALAN TP 
JOIN CHECKOUT C ON TP.TRANS_PENJUALAN_ID = C.TRANS_PENJUALAN_ID
JOIN PRODUCT P ON C.PROD_ID = P.PROD_ID
WHERE TP.TANGGAL >= '2020-12-1' AND TP.TANGGAL <= '2020-12-31'
ORDER BY TP.TANGGAL

-----BUAT GET DATA PENJUALAN---






