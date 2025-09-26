import { ChatBubbleLeftIcon, PlusIcon } from "@heroicons/react/24/outline";
import { memo, useState } from "react";
import SearchModal from "../../../components/common/Modal/SearchModal";
import { NavLink, useLocation } from "react-router-dom";
import SearchInput from "../../../components/common/Input/SearchInput";
import NavLinkItem from "./components/NavLinkItem";
import CreatePostPopover from "../../home/components/Popover/CreatePost/index";

// Desktop Header Component
const DesktopNavbar = memo(({
  navigationItems,
  setActiveTab,

}) => {

  const [openSearchModal, setOpenSearchModal] = useState(false);
  const [openCreatePostPopover, setCreatePostPopover] = useState(false);
  return (
    <header className="hidden lg:flex fixed top-0 left-0 right-0 m-1 bg-white shadow-sm z-50 border-b border-gray-200">
      <div className="max-w-screen-2xl mx-auto px-4 w-full">
        <div className="flex items-center justify-between h-14">
          {/* Left Section - Brand */}
          <div className="flex items-center space-x-4">
            <h1
              onClick={() => setActiveTab("home")}
              className="text-2xl font-extrabold text-transparent bg-clip-text bg-gradient-to-r from-indigo-600 to-blue-500 cursor-pointer hover:scale-105 transition-transform"
            >
              HandNote
            </h1>
            <SearchInput placeholder={"Search HandNote..."} onSearchClick={() => setOpenSearchModal(true)} onSearchChange={() => setOpenSearchModal(true)} />
          </div>
          {/* Center Navigation */}
          <nav className="flex items-center space-x-2">
            {
              navigationItems.map((item, index) => { return <NavLinkItem key={index} item={item} /> })
            }
          </nav>
          {/* Right Section */}
          <div className="flex items-center space-x-2">

            <button onClick={() => setCreatePostPopover(true)}
              className="p-2 bg-gray-100 rounded-full hover:bg-gray-200 transition-colors">
              <PlusIcon className="w-5 h-5 text-gray-600" />
            </button>

            <NavLink
              to={"/chat"}
              className={({ isActive }) =>
                `p-2 bg-gray-100 rounded-full hover:bg-gray-200 transition-colors ${isActive ? "border-2 border-r border-blue-600" : "border-none"}`}>
              <ChatBubbleLeftIcon className="w-5 h-5 text-gray-600" />

            </NavLink>

          </div>
        </div>
      </div>
      <SearchModal handleClose={() => setOpenSearchModal(false)}
        open={openSearchModal} />
      <CreatePostPopover handleClose={() => setCreatePostPopover(false)} open={openCreatePostPopover} />
    </header>

  );
});
export default DesktopNavbar;