# app.py

from flask import Flask, render_template
from flask_sqlalchemy import SQLAlchemy

app = Flask(__name__)

app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql://user:password@localhost/dbname'  # replace accordingly
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False

db = SQLAlchemy(app)


class Character(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(50), nullable=False)
    avatar = db.Column(db.String(255), nullable=False)
    health = db.Column(db.Integer, nullable=False)
    bio = db.Column(db.Text, nullable=False)
    color = db.Column(db.String(25), nullable=False)
    attack = db.Column(db.Integer, nullable=False)
    defense = db.Column(db.Integer, nullable=False)
    weapon = db.Column(db.String(50))
    armor = db.Column(db.String(50))


@app.route('/')
def index():
    characters = Character.query.order_by(Character.name).all()
    total_characters = Character.query.count()
    return render_template('index.html', characters=characters, total_characters=total_characters)


@app.route('/character/<int:char_id>')
def character_detail(char_id):
    character = Character.query.get_or_404(char_id)
    return render_template('character.html', character=character)


if __name__ == '__main__':
    app.run(debug=True)
