import React from "react";
import { Routes, Route } from "react-router-dom";
import NotFound from "./pages/NotFound";
import SettingsPage from "./pages/Settings";
import HomePage from "./pages/Home";
import RegisterPage from "./pages/Register";
import LoginPage from "./pages/Login";
import Profile from "./pages/Profile";
import Friends from "./pages/Friends";
import Notifications from "./pages/Notifications";
import Chat from "./pages/Chat";
import Navbar from "./features/layout/Navbar/Navbar";
import Search from "./pages/Search";

export default function App() {
  return (
    <Routes>
      <Route element={<Navbar />}>
        <Route path="/friends" element={<Friends />} />
        <Route path="/" element={<HomePage />} />
        <Route path="/settings" element={<SettingsPage />} />
        <Route path="/profile/:id" element={<Profile />} />
        <Route path="/notifications" element={<Notifications />} />
        <Route path="/chat" element={<Chat />} />
      </Route>
      <Route path="/search" element={<Search />} />
      <Route path="*" element={<NotFound />} />
      <Route path="/register" element={<RegisterPage />} />
      <Route path="/login" element={<LoginPage />} />
    </Routes>
  );

}
