document.addEventListener('DOMContentLoaded', function () {

    // Lấy tất cả các tiêu đề module
    const moduleHeaders = document.querySelectorAll('.module-header');

    // Lấy tất cả các mục bài học
    const lessonItems = document.querySelectorAll('.lesson-item');

    // Lấy phần tử video và tiêu đề
    const videoPlayer = document.getElementById('video-player');
    const lessonTitle = document.getElementById('lesson-title');

    // ===== 1. XỬ LÝ MỞ/ĐÓNG MODULE (ACCORDION) =====
    moduleHeaders.forEach(header => {
        header.addEventListener('click', function () {
            // Lấy module cha (div.module)
            const module = this.parentElement;

            // Kiểm tra xem module có bị khóa không
            if (module.classList.contains('locked')) {
                alert('Bạn cần nâng cấp tài khoản để xem nội dung này!');
                return; // Dừng lại, không làm gì cả
            }

            // Lấy danh sách bài học (ul.lesson-list) bên trong module này
            const lessonList = this.nextElementSibling;

            // Lấy icon mũi tên
            const icon = this.querySelector('.fa-chevron-down, .fa-chevron-right');

            // Toggle (bật/tắt) class 'active'
            if (lessonList.classList.contains('active')) {
                // Nếu đang mở -> Đóng lại
                lessonList.classList.remove('active');
                if (icon) icon.classList.replace('fa-chevron-down', 'fa-chevron-right');
            } else {
                // Nếu đang đóng -> Mở ra
                lessonList.classList.add('active');
                if (icon) icon.classList.replace('fa-chevron-right', 'fa-chevron-down');
            }
        });
    });

    // ===== 2. XỬ LÝ NHẤP VÀO BÀI HỌC (VIDEO SWITCH) =====
    lessonItems.forEach(item => {
        item.addEventListener('click', function () {

            // 1. Kiểm tra nếu module bị khóa
            if (this.closest('.module').classList.contains('locked')) {
                return;
            }

            // 2. Lấy thông tin từ data attributes
            const lessonType = this.getAttribute('data-lesson-type');
            const url = this.getAttribute('data-url');
            const title = this.getAttribute('data-title');

            // 3. Lấy các phần tử DOM cần cập nhật
            const contentContainer = document.getElementById('lesson-content-container');
            const lessonTitle = document.getElementById('lesson-title');

            // 4. Cập nhật tiêu đề và trạng thái 'active'
            if (lessonTitle) lessonTitle.textContent = title;

            lessonItems.forEach(i => i.classList.remove('active'));
            this.classList.add('active');

            // 5. Xử lý logic tải nội dung
            if (lessonType === 'video') {
                // Nếu là video, chúng ta gọi Partial View của video
                // (Hoặc bạn có thể dùng lại logic cũ là chỉ thay src của iframe)
                // Logic nhất quán là gọi Partial View cho cả hai
                const videoPartialUrl = AppUrls.loadVideoPartial + `?videoUrl=${encodeURIComponent(url)}`;

                loadContent(videoPartialUrl, contentContainer);

            } else if (lessonType === 'quiz') {
                // Nếu là quiz, chúng ta gọi Partial View của quiz
                // url ở đây đã là @Url.Action("LoadQuizPartial", "Course")
                loadContent(url, contentContainer);
            }
        });
    });

    // Hàm trợ giúp (helper) để tải nội dung bằng AJAX (fetch)
    function loadContent(url, container) {
        // Hiển thị loading (nếu muốn)
        container.innerHTML = '<p style="text-align:center; padding: 50px;">Đang tải...</p>';

        fetch(url)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.text(); // Lấy nội dung HTML trả về
            })
            .then(html => {
                container.innerHTML = html; // Đặt HTML vào container
            })
            .catch(error => {
                console.error('Lỗi khi tải partial view:', error);
                container.innerHTML = '<p style="text-align:center; color:red;">Tải nội dung thất bại.</p>';
            });
    }

    // ===== 3. XỬ LÝ ĐÓNG/MỞ SIDEBAR =====
    const courseContainer = document.querySelector('.course-container');
    const closeSidebarBtn = document.getElementById('close-sidebar');
    const openSidebarBtn = document.getElementById('open-sidebar');

    // 3.1. Khi nhấn nút "X" (để đóng)
    if (closeSidebarBtn) {
        closeSidebarBtn.addEventListener('click', function () {
            // Thêm lớp 'sidebar-hidden' vào container
            courseContainer.classList.add('sidebar-hidden');
        });
    }

    // 3.2. Khi nhấn nút "3 gạch" (để mở)
    if (openSidebarBtn) {
        openSidebarBtn.addEventListener('click', function () {
            // Xóa lớp 'sidebar-hidden' khỏi container
            courseContainer.classList.remove('sidebar-hidden');
        });
    }

 
});