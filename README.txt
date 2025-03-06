Trong phương thức Get của từng API, sẽ có nhưng tham số như filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize
filterOn, filterQuery có thể tích hợp vào chức năng search, ví dụ muốn filter nhứng data có name là LocDang thì filterOn = name, filterQuery = LocDang

sortBy, isAscending sẽ sắp xếp dữ liệu, ví dụ muốn sắp xếp data theo giá tăng dần thì sortBy = price, isAscending = true

pageNumber, pageSize để phân trang, hiện đang để mặc định là 10 data/page, nếu muốn khác thì truyền tham số pageSize vào, ko thì khỏi