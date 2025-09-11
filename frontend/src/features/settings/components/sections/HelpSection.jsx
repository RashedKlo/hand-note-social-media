import Button from "../../../../components/common/Button";
import SettingItem from "../settingItem";
import { QuestionMarkCircleIcon, ChatBubbleLeftRightIcon, ExclamationTriangleIcon } from '@heroicons/react/24/outline';

export default function HelpSection() {
  return (
    <div className="space-y-6">
      <header>
        <h2 className="text-2xl font-bold mb-2 text-gray-900">
          Help & Support
        </h2>
        <p className="text-gray-600">
          Get help and manage your account
        </p>
      </header>

      <SettingItem
        icon={QuestionMarkCircleIcon}
        title="Help Center"
        description="Find answers to common questions"
      >
        <Button variant="secondary">
          Visit Help Center
        </Button>
      </SettingItem>

      <SettingItem
        icon={ChatBubbleLeftRightIcon}
        title="Contact Support"
        description="Get in touch with our support team"
      >
        <div className="flex flex-wrap gap-2">
          <Button variant="secondary">
            Send Message
          </Button>
          <Button variant="secondary">
            Report a Problem
          </Button>
        </div>
      </SettingItem>

      <SettingItem
        icon={ExclamationTriangleIcon}
        title="Account Management"
        description="Download your data or deactivate your account"
        danger
      >
        <div className="flex flex-wrap gap-2">
          <Button variant="secondary">
            Download Your Data
          </Button>
          <Button variant="secondary">
            Deactivate Account
          </Button>
          <Button variant="danger">
            Delete Account
          </Button>
        </div>
      </SettingItem>
    </div>
  );
}