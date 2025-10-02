import { MagnifyingGlassIcon } from "@heroicons/react/24/outline";

const SearchInput = ({ onSearchClick, onSearchChange, placeholder, value }) => (
  <div className="relative ml-4">
    <input
      type="text"
      placeholder={placeholder}
      onClick={onSearchClick}
      onChange={onSearchChange}
      className="bg-gray-100 rounded-full py-2 pl-10 pr-4 w-60 text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:bg-white cursor-pointer"
      value={value}
    />
    <MagnifyingGlassIcon className="w-4 h-4 text-gray-500 absolute left-3 top-1/2 -translate-y-1/2" />
  </div>
);
export default SearchInput;
