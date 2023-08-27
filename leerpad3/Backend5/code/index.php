<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>All Characters</title>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
    <link href="resources/css/style.css" rel="stylesheet"/>
</head>
<body>




    <?php
    $servername = "localhost";
    $username = "root";
    $password = "";
    $dbname = "tast";

    try {
        $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
        $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    } catch(PDOException $e) {
        echo "Fout bij de databaseverbinding: " . $e->getMessage();
    }

    try {
        $stmt = $conn->query("SELECT COUNT(*) FROM characters");
        $totalCharacters = $stmt->fetchColumn();
    } catch(PDOException $e) {
        echo "Fout bij het ophalen van het totale aantal characters: " . $e->getMessage();
    }
    ?>




<header><h1>Alle <?php echo $totalCharacters; ?> characters uit de database</h1></header>

<div id="container">
    <?php

    try {
        $stmt = $conn->query("SELECT * FROM characters ORDER BY name");
        $characters = $stmt->fetchAll(PDO::FETCH_ASSOC);
    } catch(PDOException $e) {
        echo "Fout bij het ophalen van de characters: " . $e->getMessage();
    }
    
    foreach ($characters as $character) {
        $name = $character['name'];
        $avatar = $character['avatar'];
        $health = $character['health'];
        $attack = $character['attack'];
        $defense = $character['defense'];
        
        echo '<a class="item" href="character.php?name=' . urlencode($name) . '">';
        echo '    <div class="left">';
        echo '        <img class="avatar" src="resources/images/' . $avatar . '">';
        echo '    </div>';
        echo '    <div class="right">';
        echo '        <h2>' . $name . '</h2>';
        echo '        <div class="stats">';
        echo '            <ul class="fa-ul">';
        echo '                <li><span class="fa-li"><i class="fas fa-heart"></i></span> ' . $health . '</li>';
        echo '                <li><span class="fa-li"><i class="fas fa-fist-raised"></i></span> ' . $attack . '</li>';
        echo '                <li><span class="fa-li"><i class="fas fa-shield-alt"></i></span> ' . $defense . '</li>';
        echo '            </ul>';
        echo '        </div>';
        echo '    </div>';
        echo '    <div class="detailButton"><i class="fas fa-search"></i> bekijk</div>';
        echo '</a>';
    }
    ?>
</div>

<footer>&copy; Hamid 2023</footer>

</body>
</html>



