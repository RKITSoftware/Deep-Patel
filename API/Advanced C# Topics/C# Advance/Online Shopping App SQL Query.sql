SELECT 
	pro02.O02F01 AS "Id",
    pro02.O02F02 AS 'Name',
    pro02.O02F03 AS 'Buy Price',
    pro02.O02F04 AS 'Sell Price',
    pro02.O02F05 AS 'Quantity',
    pro02.O02F06 AS 'Image Link',
    cat01.T01F02 AS 'Category Name',
    sup01.P01F02 AS 'Suplier Name'
FROM
    pro02
        INNER JOIN
    cat01 ON pro02.O02F09 = cat01.T01F01
        INNER JOIN
    sup01 ON pro02.O02F10 = sup01.P01F01;
    
SELECT 
    crt01.T01F01 AS 'Id',
    pro02.O02F02 AS 'Product Name',
    crt01.T01F04 AS 'Quantity',
    (crt01.T01F05 * crt01.T01F04) AS 'Price'
FROM
    crt01
        INNER JOIN
    pro02 ON crt01.T01F03 = pro02.O02F01
WHERE
    crt01.T01F02 = 1;