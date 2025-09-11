import { useNavigate, Outlet } from "react-router-dom";
import DesktopNavbar from "./DesktopNavbar";
import MobileNavbar from "./MobileNavbar";
import { getNavigationItems } from "./navItems";

const currentUser = {
  userID: 1,
  userName: 'You',
  profilePicture: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=32&h=32&fit=crop&crop=face'
};
const navigationItems = getNavigationItems(currentUser);


function Navbar() {
  const navigate = useNavigate();
  const handleTabChange = (tabId) => {
    const pathName = navigationItems.find(({ id }) => (id == tabId)).path;
    if (pathName)
      navigate(pathName);
  };

  return (
    <>
      <DesktopNavbar
        currentUser={currentUser}
        navigationItems={navigationItems}
        setActiveTab={handleTabChange}

      />

      <MobileNavbar
        navigationItems={navigationItems}
      />
      {/* Content area with proper padding */}
      <div className="pt-14 pb-16 lg:pb-0">
        <Outlet />
      </div>


    </>
  );
}

export default Navbar;