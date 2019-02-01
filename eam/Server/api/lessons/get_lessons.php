<?php
// required headers
header('Access-Control-Allow-Origin: *');
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: OPTIONS, POST");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

if ($_SERVER['REQUEST_METHOD'] == 'OPTIONS') {
    exit;
}

// get posted data
$department_id = json_decode(file_get_contents("php://input"));

// files needed to connect to database
include_once '../config/database.php';

$database = new Database();
$db = $database->getConnection();

$query = "SELECT id, title, year, semester, department_id
            FROM  lesson
            WHERE department_id = " . $department_id;

// prepare the query
$stmt = $db->prepare($query);
$stmt->execute();

$num = $stmt->rowCount();
$result = array();
if ($num > 0) {
    while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
        $temp = array(
            "id" => $row['id'],
            "title" => $row['title'],
            "year" => $row['year'],
            "semester" => $row['semester'],
            "department_id" => $row['department_id'],
            "books" => array(),
        );

        $query1 = "SELECT books.id, title, author, description, isbn , address, map_link, phone_number, email, name
            FROM  books, book_lesson, receive_point
            WHERE book_lesson.lesson_id = " . $row['id'] . " and book_lesson.book_id = books.id and books.id = receive_point.book_id";

        // prepare the query
        $stmt1 = $db->prepare($query1);
        $stmt1->execute();

        while ($row1 = $stmt1->fetch(PDO::FETCH_ASSOC)) {
            $temp1 = array(
                "id" => $row1['id'],
                "title" => $row1['title'],
                "author" => $row1['author'],
                "description" => $row1['description'],
                "isbn" => $row1['isbn'],
                "address" => $row1['address'],
                "map_link" => $row1['map_link'],
                "phone_number" => $row1['phone_number'],
                "email" => $row1['email'],
                "name" => $row1['name'],
            );

            array_push($temp['books'], $temp1);
        }

        array_push($result, $temp);
    }

    http_response_code(200);
    echo json_encode(
        array(
            "data" => $result,
        )
    );

    // assign values to object properties
    // $this->id = $row['id'];
    // $this->firstname = $row['firstname'];
    // $this->lastname = $row['lastname'];
    // $this->password = $row['password'];
    // $this->role_id = $row['role_id'];
    // $this->department_id = $row['department_id'];
    // $this->year = $row['year'];

} else {
    // set response code
    http_response_code(400);

    // display message: unable to create user
    echo json_encode(array("error" => "Unable to get lessons."));
}
