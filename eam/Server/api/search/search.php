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
$data = json_decode(file_get_contents("php://input"));

// files needed to connect to database
include_once '../config/database.php';

$database = new Database();
$db = $database->getConnection();

$query = "SELECT books.id, title, author, description, isbn , address, map_link, phone_number, email, name
FROM  books, receive_point
WHERE books.id = receive_point.book_id and (books.title like '%" . $data->search_string . "%' or books.isbn like '" . $data->search_string . "' or books.author like '" . $data->search_string . "')";

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
            "author" => $row['author'],
            "description" => $row['description'],
            "isbn" => $row['isbn'],
            "address" => $row['address'],
            "map_link" => $row['map_link'],
            "phone_number" => $row['phone_number'],
            "email" => $row['email'],
            "name" => $row['name'],
        );

        array_push($result, $temp);
    }

    http_response_code(200);
    echo json_encode(
        array(
            "data" => $result,
        )
    );

} else {
    // set response code
    http_response_code(400);

    // display message: unable to create user
    echo json_encode(array("error" => "Unable to get lessons."));
}
