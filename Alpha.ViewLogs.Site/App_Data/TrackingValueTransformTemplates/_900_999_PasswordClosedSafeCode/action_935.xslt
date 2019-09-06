<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />
  <xsl:output method="html" indent="no" />

  <xsl:template match="cs_customer_securitycode_upd">
    <table class="tblDesc">
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'IsForcedChangeSecurityCode'" />
        <xsl:with-param name="firstValue" select="@isforcedchangesecuritycode" />

        <xsl:with-param name="secondKey" select="'ip'" />
        <xsl:with-param name="secondValue" select="@ip" />

        <xsl:with-param name="thirdKey" select="'errCode'" />
        <xsl:with-param name="thirdValue" select="@errcode" />
      </xsl:call-template>

      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'errMsg'" />
        <xsl:with-param name="firstValue" select="@errMsg" />
      </xsl:call-template>
    </table>
  </xsl:template>
</xsl:stylesheet>
