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

// files needed to connect to database
include_once '../config/database.php';

$database = new Database();
$db = $database->getConnection();

// required to decode jwt
include_once '../config/core.php';
include_once '../lib/php-jwt/src/BeforeValidException.php';
include_once '../lib/php-jwt/src/ExpiredException.php';
include_once '../lib/php-jwt/src/SignatureInvalidException.php';
include_once '../lib/php-jwt/src/JWT.php';
use \Firebase\JWT\JWT;

// get posted data
$data = json_decode(file_get_contents("php://input"));

// get jwt
$jwt = isset($data->jwt) ? $data->jwt : "";

// if jwt is not empty
if ($jwt) {

    // if decode succeed, show user details
    try {
        // decode jwt
        $decoded = JWT::decode($jwt, $key, array('HS256'));

    }
    // if decode fails, it means jwt is invalid
     catch (Exception $e) {

        // set response code
        http_response_code(401);

        // tell the user access denied  & show error message
        echo json_encode(array(
            "message" => "Access denied.",
            "error" => $e->getMessage(),
        ));
    }
}

$query = "SELECT DISTINCT lesson.title,books.title as book_title,books.author , book_lesson.id as id
FROM lesson,books,book_lesson, department
WHERE lesson.id = book_lesson.lesson_id and books.id = book_lesson.book_id and department.id = " . $decoded->data->department_id;

// prepare the query
$stmt = $db->prepare($query);
$stmt->execute();

$num = $stmt->rowCount();
$result = array();
if ($num > 0) {
    while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
        $temp = array(
            "title" => $row['title'],
            "book_title" => $row['book_title'],
            "author" => $row['author'],
            "id" => $row['id']
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
    echo json_encode(array("error" => "Δεν έχετε δηλώσει κανένα μάθημα."));
}
