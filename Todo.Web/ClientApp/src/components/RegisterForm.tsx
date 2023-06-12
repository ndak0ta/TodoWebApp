import React, { Dispatch, FormEvent, SetStateAction, useState } from "react";
import { useNavigate } from "react-router-dom";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Card from "@mui/material/Card";
import Box from "@mui/material/Box";
import axios from "axios";
import { Typography } from "@mui/material";

interface RegisterFormProps {
  setToken: Dispatch<SetStateAction<string | null>>;
  setError: Dispatch<SetStateAction<string | null>>;
}

export default function RegisterForm({
  setToken,
  setError,
}: RegisterFormProps) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const handleLogin = async (e: FormEvent) => {
    e.preventDefault();

    try {
      const response = await axios.post("/api/auth/login", {
        username,
        password,
      });
      setToken(response.data.token);
      localStorage.setItem("token", response.data.token);
      navigate("/");
    } catch (error) {
      console.error("Giriş yapılamadı:", error);
    }
  };

  const handleRegister = async (e: FormEvent) => {
    e.preventDefault();

    const response = await axios
      .post(
        "api/user",
        { username, password },
        {
          validateStatus: (status) => {
            if (status === 409) {
              setError("Bu kullanıcı adı zaten alınmış");
            }
            if (status === 200) {
              handleLogin(e);
            }
            return status >= 200 && status < 300;
          },
        }
      )
      .catch((err) => console.error(err));
  };

  return (
    <Box
      sx={{
        width: "100%",
      }}
    >
      <Card
        sx={{
          width: 300,
          margin: "auto",
          marginTop: 5,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          padding: 5,
        }}
      >
        <Typography component="h1" variant="h5">
          Kayıt Ol
        </Typography>
        <Box
          component="form"
          noValidate
          onSubmit={handleRegister}
          sx={{
            width: "150",
            paddingTop: 2,
            paddingBottom: 2,
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
        >
          <TextField
            id="outlined-basic"
            label="Kullanıcı Adı"
            variant="outlined"
            sx={{ display: "block" }}
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          <TextField
            id="outlined-basic"
            label="Şifre"
            variant="outlined"
            type="password"
            sx={{ display: "block" }}
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <Button type="submit" variant="contained">
            Kayıt Ol
          </Button>
        </Box>
      </Card>
    </Box>
  );
}
