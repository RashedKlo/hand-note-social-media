import { NavLink } from "react-router-dom";

const NavLinkItem = ({ item }) => <NavLink
  key={item.id}
  to={item.path}
  className={({ isActive }) =>
    `flex items-center justify-center flex-col  w-28 h-14 rounded-lg transition-colors relative ${isActive ? "text-blue-600" : "text-gray-600 hover:bg-gray-100"
    }`
  }
>
  {({ isActive }) => (
    <>
      {isActive ? <div className="relative">
        {item.iconSolid}
        {item.count > 0 && <span className="absolute -top-1 -right-1 flex h-4 w-4 items-center justify-center rounded-full bg-red-500 text-[10px] font-bold text-white">
          {item.count}
        </span>}
      </div> :
        <div className="relative">
          {item.icon}
          {item.count > 0 && <span className="absolute -top-1 -right-1 flex h-4 w-4 items-center justify-center rounded-full bg-red-500 text-[10px] font-bold text-white">
            {item.count}
          </span>}

        </div>
      }

      {isActive && (
        <div className="absolute bottom-0 left-0 right-0 h-0.5 bg-blue-600 rounded-t"></div>
      )}
      <span className="text-xs mt-1 mx-1">{item.label}</span>

    </>
  )}
</NavLink>;
export default NavLinkItem;