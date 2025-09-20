# Dự án E-Learning

Dự án e-learning được xây dựng trên nền tảng ASP.NET MVC và cơ sở dữ liệu Oracle.

## Yêu cầu hệ thống

- Visual Studio 2019 hoặc mới hơn.
- .NET Framework 4.7.2 Developer Pack.
- Oracle Database 11g hoặc mới hơn (hoặc có cài đặt Oracle Client tương thích).

## Hướng dẫn cài đặt và chạy dự án

1.  **Clone repository về máy:**
    ```bash
    git clone <URL_CUA_REPOSITORY>
    cd e_learning
    ```

2.  **Cấu hình Connection String:**
    Dự án cần kết nối đến cơ sở dữ liệu Oracle. Bạn cần cập nhật chuỗi kết nối ở 2 nơi:
    - Mở tệp `e_learning/Web.config`.
    - Mở tệp `Data_Oracle/App.config`.

    Tìm đến phần `<connectionStrings>` và cập nhật giá trị cho chuỗi kết nối có tên `OracleDbContext` để trỏ đến cơ sở dữ liệu Oracle của bạn.

    Ví dụ:
    ```xml
    <connectionStrings>
      <add name="OracleDbContext"
           providerName="Oracle.ManagedDataAccess.Client"
           connectionString="User Id=your_user;Password=your_password;Data Source=your_db_source;" />
    </connectionStrings>
    ```

3.  **Phục hồi các gói NuGet:**
    - Mở tệp `e_learning.sln` bằng Visual Studio.
    - Visual Studio sẽ tự động phục hồi các gói NuGet cần thiết khi bạn build dự án lần đầu. Nếu không, bạn có thể mở **Package Manager Console** (`Tools` > `NuGet Package Manager` > `Package Manager Console`) và chạy lệnh:
      ```powershell
      Update-Package -Reinstall
      ```

4.  **Cập nhật cơ sở dữ liệu (Database Migration):**
    - Trong cửa sổ **Package Manager Console** của Visual Studio:
    - Ở mục **Default project**, chọn `Data_Oracle`.
    - Chạy lệnh sau để áp dụng các migration và tạo schema cho cơ sở dữ liệu:
      ```powershell
      Update-Database
      ```

5.  **Build và chạy dự án:**
    - Trong Solution Explorer, chuột phải vào project `e_learning` và chọn **Set as StartUp Project**.
    - Nhấn `F5` hoặc nút **Start** (biểu tượng tam giác màu xanh) để build và chạy ứng dụng web.
