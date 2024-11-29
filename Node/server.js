const express = require('express');
const bodyParser = require('body-parser');
const mysql = require('mysql2');

// 서버 설정
const app = express();
const port = 3000;

// JSON 데이터를 파싱
app.use(bodyParser.json());

// MySQL 연결 설정
const db = mysql.createConnection({
  host: 'localhost',
  user: 'your_mysql_username', // MySQL 사용자 이름
  password: 'your_mysql_password', // MySQL 비밀번호
  database: 'gomoku', // 데이터베이스 이름
});

// MySQL 연결
db.connect((err) => {
  if (err) {
    console.error('MySQL 연결 실패:', err);
  } else {
    console.log('MySQL에 성공적으로 연결되었습니다.');
  }
});

// 기본 라우트 테스트
app.get('/', (req, res) => {
  res.send('Gomoku 서버 실행 중!');
});

// 서버 시작
app.listen(port, () => {
  console.log(`서버가 http://localhost:${port}에서 실행 중입니다.`);
});

// 게임 시작 API
app.post('/start-game', (req, res) => {
    const query = 'INSERT INTO Game (start_time) VALUES (NOW())';
  
    db.query(query, (err, result) => {
      if (err) {
        console.error('게임 시작 실패:', err);
        res.status(500).json({ error: '게임 시작 중 오류 발생' });
      } else {
        res.status(200).json({ message: '게임이 시작되었습니다.', gameId: result.insertId });
      }
    });
  });

  // 돌 놓기 API
app.post('/place-stone', (req, res) => {
    const { gameId, playerId, moveNumber, x, y } = req.body;
  
    const query = `
      INSERT INTO Move (game_id, player_id, move_number, x_coordinate, y_coordinate)
      VALUES (?, ?, ?, ?, ?)
    `;
  
    db.query(query, [gameId, playerId, moveNumber, x, y], (err) => {
      if (err) {
        console.error('돌 놓기 실패:', err);
        res.status(500).json({ error: '돌 놓기 중 오류 발생' });
      } else {
        res.status(200).json({ message: '돌을 성공적으로 놓았습니다.' });
      }
    });
  });

  // 승패 관리 API
app.post('/end-game', (req, res) => {
    const { gameId, winnerId } = req.body;
  
    const query = `
      UPDATE Game 
      SET end_time = NOW(), winner_id = ?
      WHERE game_id = ?
    `;
  
    db.query(query, [winnerId, gameId], (err) => {
      if (err) {
        console.error('게임 종료 실패:', err);
        res.status(500).json({ error: '게임 종료 중 오류 발생' });
      } else {
        res.status(200).json({ message: '게임이 성공적으로 종료되었습니다.' });
      }
    });
  });