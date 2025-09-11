import { MagnifyingGlassIcon, UserPlusIcon, XMarkIcon } from "@heroicons/react/24/outline";
import { useState } from "react";
import EmptyState from "../EmptyState/EmptyState";
const Header = ({ searchTerm, setSearchTerm }) => {
    const onAddFriend = () => { };
    const filterBySearch = (list) => {
        if (!searchTerm.trim()) return list;
        return list.filter((person) => person.name.toLowerCase().includes(searchTerm.toLowerCase()));
    };
    const data = [];
    return <div className="sticky top-0 z-10 border-b border-gray-200 bg-white">
        <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
            <div className="flex flex-col gap-4 py-4 sm:flex-row sm:items-center sm:justify-between lg:py-6">
                <div className="flex items-center justify-between">
                    <div>
                        <h1 className="text-xl font-bold text-gray-900 lg:text-2xl">Friends</h1>
                    </div>
                    <button onClick={onAddFriend} className="btn-primary p-2 sm:hidden">
                        <UserPlusIcon className="h-5 w-5" />
                    </button>
                </div>
                {/* search  */}
                <div className="flex items-center gap-3">
                    <div className="relative flex-1 sm:w-64 sm:flex-none lg:w-80">
                        <MagnifyingGlassIcon className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 transform text-gray-400" />
                        <input
                            type="text"
                            placeholder="Search friends..."
                            value={searchTerm}
                            onChange={(e) => setSearchTerm(e.target.value)}
                            className="input-field w-full pl-10 pr-4 text-sm"
                        />
                        {searchTerm && (
                            <button
                                onClick={() => setSearchTerm('')}
                                className="absolute right-3 top-1/2 -translate-y-1/2 transform text-gray-400 hover:text-gray-600"
                            >
                                <XMarkIcon className="h-4 w-4" />
                            </button>
                        )}
                    </div>
                    <button
                        onClick={onAddFriend}
                        className="btn-primary hidden items-center gap-2 text-sm font-medium sm:flex"
                    >
                        <UserPlusIcon className="h-4 w-4" />
                        <span className="hidden lg:inline">Add Friend</span>
                    </button>
                </div>
            </div>
        </div>
    </div>
};
export default Header;