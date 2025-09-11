
import { memo } from "react";
import NavLinkItem from "./components/NavLinkItem";


const MobileNavbar = memo(({ navigationItems }) => (
  <header className="lg:hidden fixed top-0 left-0 right-0 bg-white border-b border-gray-200 z-40">
    <div className="flex items-center justify-around pt-2">
      {navigationItems.map((item,index) => {
        return (
        <NavLinkItem key={index} item={item}/>
        );
      })}
    </div>
  </header>
));
export default MobileNavbar; 