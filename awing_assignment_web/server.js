const express = require("express");
const cors = require("cors");

const app = express();

// Enable CORS for all routes
app.use(cors());

// For specific origins:
app.use(
  cors({
    origin: "http://localhost:3000", // Allow only this origin
  })
);

// Example route
app.post("/main/navigate", (req, res) => {
  res.json({
    data: "Program running",
    succeed: true,
    errors: null,
    message: "",
  });
});

app.listen(7284, () => console.log("Server running on port 7284"));
