import {
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
  CardFooter,
} from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { login } from "./api/financeTrackerAPI";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
function Login() {
  const navigate = useNavigate();
  const [loginData, setLoginData] = useState({ userName: "", password: "" });

  const loginMutation = useMutation({
    mutationFn: login,
    onSuccess: (data) => {
      localStorage.setItem("token", data.token);
      navigate("/financeTrackerAPI2");
    },
  });
  const handleSubmit = (e) => {
    e.preventDefault();
    loginMutation.mutate(loginData);
  };
  return (
    <div>
      <Card className="w-96">
        <CardHeader>
          <CardTitle>Login</CardTitle>
        </CardHeader>

        <CardContent>
          <form onSubmit={handleSubmit} className="space-y-4">
            <Input
              placeholder="Username"
              value={loginData.userName}
              autoComplete="username"
              onChange={(e) =>
                setLoginData({
                  ...loginData,
                  userName: e.target.value,
                })
              }
            />
            <Input
              type="password"
              placeholder="Password"
              value={loginData.password}
              autoComplete="current-password"
              onChange={(e) =>
                setLoginData({
                  ...loginData,
                  password: e.target.value,
                })
              }
            />
            <Button type="submit" disabled={loginMutation.isPending}>
              {loginMutation.isPending ? "Logging in..." : "Login"}
            </Button>

            {loginMutation.isError && <p>Invalid username or password.</p>}
          </form>
        </CardContent>
      </Card>
    </div>
  );
}

export default Login;
