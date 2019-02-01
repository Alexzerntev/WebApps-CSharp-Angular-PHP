<?php
// 'user' object
class User
{

    // database connection and table name
    private $conn;
    private $table_name = "users";

    // object properties
    public $id;
    public $firstname;
    public $lastname;
    public $email;
    public $password;
    public $role_id;
    public $department_id;
    public $year;
    public $phone_number;

    // constructor
    public function __construct($db)
    {
        $this->conn = $db;
    }

    public function create()
    {

        // insert query
        $query = "INSERT INTO " . $this->table_name . "
			SET
			firstname = :firstname,
			lastname = :lastname,
			email = :email,
            password = :password,
            role_id = :role_id,
            department_id = :department_id,
            year = :year,
            phone_number = :phone_number";

        // prepare the query
        $stmt = $this->conn->prepare($query);

        // sanitize
        $this->firstname = htmlspecialchars(strip_tags($this->firstname));
        $this->lastname = htmlspecialchars(strip_tags($this->lastname));
        $this->email = htmlspecialchars(strip_tags($this->email));
        $this->password = htmlspecialchars(strip_tags($this->password));
        $this->role_id = htmlspecialchars(strip_tags($this->role_id));
        $this->department_id = htmlspecialchars(strip_tags($this->department_id));
        $this->year = htmlspecialchars(strip_tags($this->year));
        $this->phone_number = htmlspecialchars(strip_tags($this->phone_number));

        // bind the values
        $stmt->bindParam(':firstname', $this->firstname);
        $stmt->bindParam(':lastname', $this->lastname);
        $stmt->bindParam(':email', $this->email);
        $stmt->bindParam(':role_id', $this->role_id);
        $stmt->bindParam(':department_id', $this->department_id);
        $stmt->bindParam(':year', $this->year);
        $stmt->bindParam(':phone_number', $this->phone_number);

        // hash the password before saving to database
        $password_hash = password_hash($this->password, PASSWORD_BCRYPT);
        $stmt->bindParam(':password', $password_hash);

        // execute the query, also check if query was successful
        if ($stmt->execute()) {
            return true;
        }

        return false;
    }

    // check if given email exist in the database
    public function emailExists()
    {

        // query to check if email exists
        $query = "SELECT id, firstname, lastname, password, role_id, department_id, year, phone_number
            FROM " . $this->table_name . "
            WHERE email = ?
            LIMIT 0,1";

        // prepare the query
        $stmt = $this->conn->prepare($query);

        // sanitize
        $this->email = htmlspecialchars(strip_tags($this->email));

        // bind given email value
        $stmt->bindParam(1, $this->email);

        // execute the query
        $stmt->execute();

        // get number of rows
        $num = $stmt->rowCount();

        // if email exists, assign values to object properties for easy access and use for php sessions
        if ($num > 0) {

            // get record details / values
            $row = $stmt->fetch(PDO::FETCH_ASSOC);

            // assign values to object properties
            $this->id = $row['id'];
            $this->firstname = $row['firstname'];
            $this->lastname = $row['lastname'];
            $this->password = $row['password'];
            $this->role_id = $row['role_id'];
            $this->department_id = $row['department_id'];
            $this->year = $row['year'];
            $this->phone_number = $row['phone_number'];

            // return true because email exists in the database
            return true;
        }

        // return false if email does not exist in the database
        return false;
    }

// update a user record
    public function update()
    {

        // if password needs to be updated
        $password_set = !empty($this->password) ? ", password = :password" : "";

        // if no posted password, do not update the password
        $query = "UPDATE " . $this->table_name . "
            SET
                firstname = :firstname,
                lastname = :lastname,
                email = :email
                {$password_set}
            WHERE id = :id";

        // prepare the query
        $stmt = $this->conn->prepare($query);

        // sanitize
        $this->firstname = htmlspecialchars(strip_tags($this->firstname));
        $this->lastname = htmlspecialchars(strip_tags($this->lastname));
        $this->email = htmlspecialchars(strip_tags($this->email));

        // bind the values from the form
        $stmt->bindParam(':firstname', $this->firstname);
        $stmt->bindParam(':lastname', $this->lastname);
        $stmt->bindParam(':email', $this->email);

        // hash the password before saving to database
        if (!empty($this->password)) {
            $this->password = htmlspecialchars(strip_tags($this->password));
            $password_hash = password_hash($this->password, PASSWORD_BCRYPT);
            $stmt->bindParam(':password', $password_hash);
        }

        // unique ID of record to be edited
        $stmt->bindParam(':id', $this->id);

        // execute the query
        if ($stmt->execute()) {
            return true;
        }

        return false;
    }
}
