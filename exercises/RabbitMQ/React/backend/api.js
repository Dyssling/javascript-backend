const express = require('express');
const amqp = require('amqplib');
const cors = require('cors');

const app = express();

app.use(express.json());
app.use(cors());

const exchange = "subscriber";

async function connectRabbitMQ(){
    const connection = await amqp.connect("amqp://guest:guest@localhost:5672");
    const channel = await connection.createChannel();

    await channel.assertExchange(exchange, "direct", {durable: false});
    return {channel};
}

app.post('/api/publish', async (req, res) => {
    const {payload} = req.body;

    const {channel} = await connectRabbitMQ();
    channel.publish(exchange, "newsletter", Buffer.from(payload) );
    res.send({message: "You are subscribed"});
})

app.listen(5000, () => console.log("Backend API running"));