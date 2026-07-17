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
import { register } from "./api/financeTrackerAPI";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
function Register() {
  const navigate = useNavigate();
  const [registerData, setRegisterData] = useState({
    userName: "",
    password: "",
  });

  const registerMutation = useMutation({
    mutationFn: register,
    onSuccess: (data) => {
      navigate("/login");
    },
  });
  const handleSubmit = (e) => {
    e.preventDefault();
    registerMutation.mutate(registerData);
  };
  return (
    <div>
      <Card className="w-96">
        <CardHeader>
          <CardTitle>Register</CardTitle>
        </CardHeader>

        <CardContent>
          <form onSubmit={handleSubmit} className="space-y-4">
            <Label htmlFor="username">Username</Label>
            <Input
              id="username"
              placeholder="Username"
              value={registerData.userName}
              autoComplete="username"
              onChange={(e) =>
                setRegisterData({
                  ...registerData,
                  userName: e.target.value,
                })
              }
            />
            <Label htmlFor="password">Password</Label>
            <Input
              id="password"
              type="password"
              placeholder="Password"
              value={registerData.password}
              autoComplete="new-password"
              onChange={(e) =>
                setRegisterData({
                  ...registerData,
                  password: e.target.value,
                })
              }
            />
            <Button type="submit" disabled={registerMutation.isPending}>
              {registerMutation.isPending ? "Registering..." : "Register"}
            </Button>

            {registerMutation.isError && (
              <p>Registration failed. Please try again.</p>
            )}
          </form>
        </CardContent>
      </Card>
    </div>
  );
}

export default Register;
