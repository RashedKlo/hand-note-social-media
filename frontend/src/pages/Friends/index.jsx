import { useState } from "react";
import MainContent from "../../features/friends/components/Content/MainContent";
import Header from "../../features/friends/components/Header/Header";


const Friends = () => {
  const [searchTerm, setSearchTerm] = useState('');

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="pt-14 lg:pt-0">
        <Header searchTerm={searchTerm} setSearchTerm={setSearchTerm} />
        <MainContent searchTerm={searchTerm} />
      </div>
    </div>
  );
};

export default Friends;