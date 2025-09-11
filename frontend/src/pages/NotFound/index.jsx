import React from "react";
import { Link } from "react-router-dom";

export default function NotFound() {
  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-gradient-to-r from-indigo-500 to-purple-600 p-8">
      <h1 className="text-9xl font-extrabold text-white drop-shadow-lg mb-6">404</h1>
      <p className="text-2xl sm:text-3xl font-semibold text-white mb-4">
        Oops! Page Not Found
      </p>
      <p className="text-white/80 mb-8 max-w-md text-center">
        The page you're looking for doesn't exist or has been moved.
      </p>
      <Link
        to="/"
        className="px-6 py-3 bg-white text-purple-600 font-semibold rounded-lg shadow-lg hover:bg-purple-100 transition"
      >
        Go Back Home
      </Link>
    </div>
  );
}
